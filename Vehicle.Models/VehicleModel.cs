﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Common;

namespace Vehicle.Models
{
    public class VehicleModel : IVehicleModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }


        public int VehicleMakeID { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }

    }
}