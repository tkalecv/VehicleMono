using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models;
using Vehicle.Models.Common;
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

        public IPagedList<IVehicleModel> GetAll(string sortOrder, string searchString, int? page)
        {
            var list = context.VehicleModels.AsEnumerable();

            //Search
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Name.Contains(searchString)
                                || x.Abrv.Contains(searchString));
            }

            //Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(x => x.Name);
                    break;
                case "Abrv":
                    list = list.OrderByDescending(x => x.Abrv);
                    break;
                case "Vehicle_makes":
                    list = list.OrderByDescending(x => x.VehicleMake);
                    break;
                default:
                    list = list.OrderBy(x => x.ID);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return list.ToPagedList(pageNumber, pageSize);

        }

        public IVehicleModel FindByID(int? id)
        {
            return context.VehicleModels.Where(x => x.ID == id).FirstOrDefault();

        }
    }
}
