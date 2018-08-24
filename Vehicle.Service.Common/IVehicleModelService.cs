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
   public interface IVehicleModelService
    {
        void Create(IVehicleModel VModel);
        void Update(IVehicleModel VModel);
        void Delete(IVehicleModel VModel);
        IPagedList<IVehicleModel> GetAll(ISorting sort, IFiltering search, IPaging paging);
        IVehicleModel FindByID(int? id);
    }
}
