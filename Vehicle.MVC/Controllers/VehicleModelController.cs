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
        public ActionResult ListAll(string Sort, int? Page, string Search)
        {
            var SortParameter = new Sorting()
            { sortOrder = Sort };
            var PagingParameter = new Paging
            { page=Page ?? 1, pageSize = 5 };
            var FilterParameter = new Filtering
            { searchString = Search };

            var VModelList = service.GetAll(SortParameter, FilterParameter, PagingParameter);

            ViewBag.Search = Search;

            ViewBag.NameSort = string.IsNullOrEmpty(Sort) ? "name_desc" : "";
            ViewBag.AbrvSort = Sort == "Abrv" ? "Abrv_desc" : "Abrv";
            ViewBag.CurrentSort = Sort;

            var VModelViewModelList = AutoMapper.Mapper.Map<IEnumerable<VehicleModelViewModel>>(VModelList);

            return View(new StaticPagedList<VehicleModelViewModel>(VModelViewModelList, VModelList.GetMetaData()));
        }

        //GET: Create VehicleModel
        public ActionResult Create()
        {
            var ModelList = new VehicleMakeListViewModel();

            return View(ModelList);
        }

        //POST: Create VehicleModel
        [HttpPost]
        public ActionResult Create(VehicleModelViewModel VModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    service.Create(AutoMapper.Mapper.Map<VehicleModel>(VModel));
                    return RedirectToAction("ListAll");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again!");
            }

            return View(VModel);
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
        public ActionResult Edit(VehicleModelViewModel VModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    service.Update(AutoMapper.Mapper.Map<VehicleModel>(VModel));
                    return RedirectToAction("ListAll");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again!");
            }
            return RedirectToAction("Edit", VModel.ID);
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
        public ActionResult Delete(VehicleModelViewModel VModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    service.Delete(AutoMapper.Mapper.Map<VehicleModel>(VModel));
                    return RedirectToAction("ListAll");
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to delete. Try again!");
            }

            return RedirectToAction("Delete", VModel.ID);
        }
    }
}