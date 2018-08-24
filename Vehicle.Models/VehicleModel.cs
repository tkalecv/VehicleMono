using System;
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

        //Foreign Key
        public int VehicleMakeID { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }
        public int? MakeID { get; set; }

    }
}
