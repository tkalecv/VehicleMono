using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehicle.Models.PagingSortingFiltering;
using Vehicle.MVC.ViewModels;
using Vehicle.Service;
using PagedList;
using Vehicle.Models;
using System.Data;
using System.Net;

namespace Vehicle.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        VehicleMakeService service = new VehicleMakeService();

        public ActionResult Index()
        {
            return View();
        }

        //GET: Paged list of all VehicleMakes
        public ActionResult ListAll(string sort, int? page, string search, string currentFilter)
        {
            var sortParameter = new Sorting()
            { sortOrder = sort };
            var filterParameter = new Filtering
            { searchString = search };
            var pagingParameter = new Paging
            { page = page ?? 1, pageSize = 3 };

            var vMakeList = service.GetAll(sortParameter, filterParameter, pagingParameter);

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;



            ViewBag.Search = search;

            ViewBag.NameSort = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.AbrvSort = sort == "Abrv" ? "Abrv_desc" : "Abrv";
            ViewBag.CurrentSort = sort;

            var vMakeListViewModel = AutoMapper.Mapper.Map<IEnumerable<VehicleMakeViewModel>>(vMakeList);

            return View(new StaticPagedList<VehicleMakeViewModel>(vMakeListViewModel, vMakeList.GetMetaData()));
        }

        //GET: Create VehicleMake
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create VehicleMake
        [HttpPost]
        public ActionResult Create(VehicleMakeViewModel vMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Create(AutoMapper.Mapper.Map<VehicleMake>(vMake));
                    return RedirectToAction("ListAll");
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again!");
            }
            return View(vMake);
        }

        //GET: Find VehicleMake by id and edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return View(AutoMapper.Mapper.Map<VehicleMakeViewModel>(service.FindByID(id)));
            }


        }

        //POST: Edit VehicleMake and save changes
        [HttpPost]
        public ActionResult Edit(VehicleMakeViewModel vMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Update(AutoMapper.Mapper.Map<VehicleMake>(vMake));

                    return RedirectToAction("ListAll");

                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again!");
            }
            return RedirectToAction("Edit", vMake.ID);
        }

        //GET: Find VehicleMake by id and delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return View(AutoMapper.Mapper.Map<VehicleMakeViewModel>(service.FindByID(id)));
            }
        }

        //POST: Delete VehicleMake and save changes
        [HttpPost]
        public ActionResult Delete(VehicleMakeViewModel vMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Delete(AutoMapper.Mapper.Map<VehicleMake>(vMake));

                    return RedirectToAction("ListAll");

                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Unable to delete. Try again!");
            }
            return RedirectToAction("Delete", vMake.ID);
        }
    }

}
