using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehicle.Models.PagingSortingFiltering;
using Vehicle.MVC.ViewModels;
using Vehicle.Service;
using PagedList;

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
            { searchString = search};
            var pagingParameter = new Paging
            { page = Page ?? 1, pageSize = 3};

            var vMakeList = service.GetAll(sortParameter, filterParameter, pagingParameter);

            ViewBag.search = search;

            ViewBag.NameSort = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.AbrvSort = sort == "Abrv" ? "Abrv_desc" : "Abrv";
            ViewBag.CurrentSort = sort;

            var vMakeListViewModel = AutoMapper.Mapper.Map<IEnumerable<VehicleMakeViewModel>>(vMakeList);

            return View(new StaticPagedList<VehicleMakeViewModel>(vMakeListViewModel, vMakeList.GetMetaData()));
        }
    }
}