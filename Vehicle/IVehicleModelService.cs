using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Common;

namespace Vehicle
{
   public interface IVehicleModelService
    {
        void Create(IVehicleModel vModel);
        void Update(IVehicleModel vModel);
        void Delete(IVehicleModel vModel);
        IList<IVehicleModel> GetAll();
        IVehicleModel FindByID(int? id);
    }
}
