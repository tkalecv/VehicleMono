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
        public ActionResult ListAll(string sort, int? Page, string search)
        {
            var SortParameter = new Sorting()
            { sortOrder = sort };
            var PagingParameter = new Paging
            { page=Page ?? 1, pageSize = 3 };
            var FilterParameter = new Filtering
            { searchString = search };

            var VModelList = service.GetAll(SortParameter, FilterParameter, PagingParameter);

            ViewBag.Search = search;

            ViewBag.NameSort = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.AbrvSort = sort == "Abrv" ? "Abrv_desc" : "Abrv";
            ViewBag.CurrentSort = sort;

            var VModelListViewModel = AutoMapper.Mapper.Map<IEnumerable<VehicleModelViewModel>>(VModelList);

            return View(new StaticPagedList<VehicleModelViewModel>(VModelListViewModel, VModelList.GetMetaData()));
        }

        //GET: Create VehicleModel
        public ActionResult Create()
        {
            VehicleMakeListViewModel ModelList = new VehicleMakeListViewModel();
            //ModelList.Items = AutoMapper.Mapper.Map<IList<SelectListItem>>(service.VehicleMakeList().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name } ));

            ModelList.Items = service.VehicleMakeList().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name });

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