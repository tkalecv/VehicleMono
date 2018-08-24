using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Common;

namespace Vehicle.Models
{
    public class VehicleMake : IVehicleMake
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        //Collection of VehicleModels - One To Many Relationship
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }

    }
}
