using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Paging__Sorting__Filtering;

namespace Vehicle
{
   public interface IVehicleMakeService
    {
        void Create(IVehicleMake vMake);
        void Update(IVehicleMake vMake);
        void Delete(IVehicleMake vMake);
        IPagedList<IVehicleMake> GetAll(ISorting sort, IFiltering search, IPaging paging);
        IVehicleMake FindByID(int? id);
    }
}
