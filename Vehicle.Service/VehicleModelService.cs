using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Paging__Sorting__Filtering;
using Vehicle.Models.Context;

namespace Vehicle.Service
{
   public class VehicleModelService : IVehicleModelService
    {
        
        VehicleContext context = new VehicleContext();

        public void Create(IVehicleModel vModel)
        {
            context.VehicleModels.Add(AutoMapper.Mapper.Map<VehicleModel>(vModel));
            context.SaveChanges();
        }

        public void Update(IVehicleModel vModel)
        {
            context.Entry(vModel).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(IVehicleModel vModel)
        {
            context.VehicleModels.Remove(context.VehicleModels.Where(x=> x.ID == vModel.ID).FirstOrDefault());
            context.SaveChanges();
        }

        public IPagedList<IVehicleModel> GetAll(ISorting sort, IFiltering search, IPaging paging)
        {
            var list = context.VehicleModels.AsEnumerable();

            //Search
            if (!String.IsNullOrEmpty(search.searchString))
            {
                list = list.Where(x => x.Name.ToUpper().StartsWith(search.searchString.ToUpper())
                                || x.Abrv.ToUpper().StartsWith(search.searchString.ToUpper()));
            }

            //Sorting
            switch (sort.sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(x => x.Name);
                    break;
                case "Abrv":
                    list = list.OrderBy(x => x.Abrv);
                    break;
                case "Abrv_desc":
                    list = list.OrderByDescending(x => x.Abrv);
                    break;
                default:
                    list = list.OrderBy(x => x.Name);
                    break;
            }

            return list.ToPagedList(paging.page, paging.pageSize);

        }

        public IVehicleModel FindByID(int? id)
        {
            return context.VehicleModels.Where(x => x.ID == id).FirstOrDefault();

        }

        public IEnumerable<IVehicleMake> VehicleMakeList()
        {
            return context.VehicleMakes.ToList();
        }
    }
}
