using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Vehicle.Models;
using Vehicle.Service;

namespace Vehicle.MVC.ViewModels
{
    //ViewModel for VehicleModel entity
    public class VehicleModelViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        [DisplayName("Vehicle Make")]
        public int VehicleMakeID { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }
    }
}