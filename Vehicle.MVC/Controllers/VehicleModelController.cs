using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
    }
}