using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehicle.Models;
using Vehicle.Models.Common;
using Vehicle.Service;

namespace Vehicle.MVC.ViewModels
{
    //ViewModel for SelectList of VehicleModels
    public class VehicleMakeListViewModel : VehicleModelViewModel
    {
       public static VehicleModelService service = new VehicleModelService();

        public IEnumerable<IVehicleMake> MakeList = service.VehicleMakeList();

    }
}