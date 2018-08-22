using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vehicle.Models;
using Vehicle.Service;

namespace Vehicle.MVC.ViewModels
{
    public class VehicleModelViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public int VehicleMakeID { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }
    }
}