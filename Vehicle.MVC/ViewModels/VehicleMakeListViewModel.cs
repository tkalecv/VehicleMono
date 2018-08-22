using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vehicle.MVC.ViewModels
{
    public class VehicleMakeListViewModel : VehicleModelViewModel
    {
        public IEnumerable<SelectListItem> Items { get; set; }
    }
}