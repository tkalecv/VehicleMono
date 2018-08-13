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
        void Create(IVehicleModel vModel);
        void Update(IVehicleModel vModel);
        void Delete(IVehicleModel vModel);
        IPagedList<IVehicleModel> GetAll(ISorting sort, IFiltering search, IPaging paging);
        IVehicleModel FindByID(int? id);
    }
}
