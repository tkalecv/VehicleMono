using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vehicle.Models;

namespace Vehicle.MVC.ViewModels
{
    //ViewModek for VehicleMake entity
    public class VehicleMakeViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}