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
        VehicleMakeService Makeservice = new VehicleMakeService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAll(string sort, int? Page, string search)
        {
            var sortParameter = new Sorting()
            { sortOrder = sort };
            var pagingParameter = new Paging
            { page=Page ?? 1, pageSize = 3 };
            var filterParameter = new Filtering
            { searchString = search };

            var vModelList = service.GetAll(sortParameter, filterParameter, pagingParameter);

            ViewBag.search = search;

            ViewBag.NameSort = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.AbrvSort = sort == "Abrv" ? "Abrv_desc" : "Abrv";
            ViewBag.CurrentSort = sort;

            var vModelListViewModel = AutoMapper.Mapper.Map < IEnumerable<VehicleModelViewModel>>(vModelList);

            return View(new StaticPagedList<VehicleModelViewModel>(vModelListViewModel, vModelList.GetMetaData()));
        }

        public ActionResult Create()
        {
            VehicleModelViewModel vModel = new VehicleModelViewModel();
            vModel.Items = Makeservice.ListItems().Select(x => new SelectListItem { Value = x.ID.ToString(), Text = x.Name });

            return View(vModel);
        }

        [HttpPost]
        public ActionResult Create(VehicleModelViewModel vModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    vModel.VehicleMakeID = vModel.SelectedID.ElementAt(0);
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