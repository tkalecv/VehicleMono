using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehicle.Models;
using Vehicle.Models.PagingSortingFiltering;
using Vehicle.MVC.ViewModels;
using Vehicle.Service;

namespace Vehicle.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        VehicleModelService service = new VehicleModelService();

        public ActionResult Index()
        {
            return View();
        }

        //GET: Paged list of all VehicleModels
        public ActionResult ListAll(string sort, int? page, string search, string currentFilter)
        {
            var sortParameter = new Sorting()
            { sortOrder = sort };
            var pagingParameter = new Paging
            { page=page ?? 1, pageSize = 5 };
            var filterParameter = new Filtering
            { searchString = search };

            var vModelList = service.GetAll(sortParameter, filterParameter, pagingParameter);

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

            var vModelViewModelList = AutoMapper.Mapper.Map<IEnumerable<VehicleModelViewModel>>(vModelList);

            return View(new StaticPagedList<VehicleModelViewModel>(vModelViewModelList, vModelList.GetMetaData()));
        }

        //GET: Create VehicleModel
        public ActionResult Create()
        {
            var ModelList = new VehicleMakeListViewModel();

            return View(ModelList);
        }

        //POST: Create VehicleModel
        [HttpPost]
        public ActionResult Create(VehicleModelViewModel vModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    service.Create(AutoMapper.Mapper.Map<VehicleModel>(vModel));
                    return RedirectToAction("ListAll");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again!");
            }

            return View(vModel);
        }

        //GET: Find VehicleModel by id and edit
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return View(AutoMapper.Mapper.Map<VehicleModelViewModel>(service.FindByID(id)));
            }
        }
        //POST: Edit VehicleModel and save changes
        [HttpPost]
        public ActionResult Edit(VehicleModelViewModel vModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    service.Update(AutoMapper.Mapper.Map<VehicleModel>(vModel));
                    return RedirectToAction("ListAll");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again!");
            }
            return RedirectToAction("Edit", vModel.ID);
        }

        //GET: Find VehicleModel by id and delete
        public ActionResult Delete(int? id)
        {
                if(id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    return View(AutoMapper.Mapper.Map<VehicleModelViewModel>(service.FindByID(id)));
                }
        }
        //POST: Delete VehicleModel and save changes
        [HttpPost]
        public ActionResult Delete(VehicleModelViewModel vModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    service.Delete(AutoMapper.Mapper.Map<VehicleModel>(vModel));
                    return RedirectToAction("ListAll");
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to delete. Try again!");
            }

            return RedirectToAction("Delete", vModel.ID);
        }
    }
}