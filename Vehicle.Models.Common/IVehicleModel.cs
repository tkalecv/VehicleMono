using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Models.Common
{
    public interface IVehicleModel
    {
        int ID { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }

        int VehicleMakeID { get; set; }
    }
}
