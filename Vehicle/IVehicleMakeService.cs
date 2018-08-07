using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Common;

namespace Vehicle
{
   public interface IVehicleMakeService
    {
        void Create(IVehicleMake vMake);
        void Update(IVehicleMake vMake);
        void Delete(IVehicleMake vMake);
        IPagedList<IVehicleMake> GetAll(string sortOrder, string searchString, int? page);
        IVehicleMake FindByID(int? id);
    }
}
