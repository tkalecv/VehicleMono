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


        public ActionResult ListAll(string sort, int? Page, string search)
        {
            var sortParameter = new Sorting()
            { sortOrder = sort };
            var filterParameter = new Filtering
            { searchString = search };
            var pagingParameter = new Paging
            { page = Page ?? 1, pageSize = 3 };

            var vMakeList = service.GetAll(sortParameter, filterParameter, pagingParameter);


            ViewBag.search = search;

            ViewBag.NameSort = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.AbrvSort = sort == "Abrv" ? "Abrv_desc" : "Abrv";
            ViewBag.CurrentSort = sort;

            var vMakeListViewModel = AutoMapper.Mapper.Map<IEnumerable<VehicleMakeViewModel>>(vMakeList);

            return View(new StaticPagedList<VehicleMakeViewModel>(vMakeListViewModel, vMakeList.GetMetaData()));
        }

        public ActionResult Create()
        {
            return View();
        }

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

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return View(AutoMapper.Mapper.Map<VehicleMakeViewModel>(service.FindByID(id)));
            }


        }

        [HttpPost]
        public ActionResult Edit(VehicleMakeViewModel vMake)
        {
            try
            {
                if(ModelState.IsValid)
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

    }
}