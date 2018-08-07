using PagedList;
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
        IPagedList<IVehicleModel> GetAll(string sortOrder, string searchString, int? page);
        IVehicleModel FindByID(int? id);
    }
}
