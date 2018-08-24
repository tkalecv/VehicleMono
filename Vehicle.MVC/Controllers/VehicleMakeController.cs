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
        public ActionResult ListAll(string Sort, int? Page, string Search)
        {
            var SortParameter = new Sorting()
            { sortOrder = Sort };
            var FilterParameter = new Filtering
            { searchString = Search };
            var PagingParameter = new Paging
            { page = Page ?? 1, pageSize = 3 };

            var VMakeList = service.GetAll(SortParameter, FilterParameter, PagingParameter);


            ViewBag.Search = Search;

            ViewBag.NameSort = string.IsNullOrEmpty(Sort) ? "name_desc" : "";
            ViewBag.AbrvSort = Sort == "Abrv" ? "Abrv_desc" : "Abrv";
            ViewBag.CurrentSort = Sort;

            var VMakeListViewModel = AutoMapper.Mapper.Map<IEnumerable<VehicleMakeViewModel>>(VMakeList);

            return View(new StaticPagedList<VehicleMakeViewModel>(VMakeListViewModel, VMakeList.GetMetaData()));
        }

        //GET: Create VehicleMake
        public ActionResult Create()
        {
            return View();
        }

        //POST: Create VehicleMake
        [HttpPost]
        public ActionResult Create(VehicleMakeViewModel VMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Create(AutoMapper.Mapper.Map<VehicleMake>(VMake));
                    return RedirectToAction("ListAll");
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again!");
            }
            return View(VMake);
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
        public ActionResult Edit(VehicleMakeViewModel VMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Update(AutoMapper.Mapper.Map<VehicleMake>(VMake));

                    return RedirectToAction("ListAll");

                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again!");
            }
            return RedirectToAction("Edit", VMake.ID);
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
        public ActionResult Delete(VehicleMakeViewModel VMake)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    service.Delete(AutoMapper.Mapper.Map<VehicleMake>(VMake));

                    return RedirectToAction("ListAll");

                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Unable to delete. Try again!");
            }
            return RedirectToAction("Delete", VMake.ID);
        }
    }

}
