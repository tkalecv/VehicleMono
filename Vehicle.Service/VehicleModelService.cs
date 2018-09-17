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

        //Add new VehicleModel
        public void Create(IVehicleModel VModel)
        {
            context.VehicleModels.Add(AutoMapper.Mapper.Map<VehicleModel>(VModel));
            context.SaveChanges();
        }

        //Update already existing VehicleModel

        public void Update(IVehicleModel VModel)
        {
            context.Entry(VModel).State = EntityState.Modified;
            context.SaveChanges();
        }

        //Find VehicleModel by ID and delete it
        public void Delete(IVehicleModel VModel)
        {
            context.VehicleModels.Remove(context.VehicleModels.Where(x=> x.ID == VModel.ID).FirstOrDefault());
            context.SaveChanges();
        }

        //Paged list of all VehicleModels with sorting and filtering
        public IPagedList<IVehicleModel> GetAll(ISorting sort, IFiltering search, IPaging paging)
        {
            IQueryable<VehicleModel> modelList;

            //Search
            if (!String.IsNullOrEmpty(search.searchString))
            {
                modelList = context.VehicleModels.Where(x => x.Name.ToUpper().StartsWith(search.searchString.ToUpper())
                                || x.Abrv.ToUpper().StartsWith(search.searchString.ToUpper()));
            }
            else
            {
                modelList = context.VehicleModels;
            }

            IPagedList<VehicleModel> vehicleModelList;

            //Sorting
            switch (sort.sortOrder)
            {
                case "name_desc":
                    vehicleModelList = modelList.OrderByDescending(x => x.Name).ToPagedList(paging.page, paging.pageSize);
                    break;
                case "Abrv":
                    vehicleModelList = modelList.OrderBy(x => x.Abrv).ToPagedList(paging.page, paging.pageSize);
                    break;
                case "Abrv_desc":
                    vehicleModelList = modelList.OrderByDescending(x => x.Abrv).ToPagedList(paging.page, paging.pageSize);
                    break;
                default:
                    vehicleModelList = modelList.OrderBy(x => x.Name).ToPagedList(paging.page, paging.pageSize);
                    break;
            }

            return (vehicleModelList);

        }

        //Find one VehicleModel by ID
        public IVehicleModel FindByID(int? id)
        {
            return context.VehicleModels.Where(x => x.ID == id).FirstOrDefault();

        }

        //List of all VehicleMakes(used for SelectList for create VehicleModel)
        public IEnumerable<IVehicleMake> VehicleMakeList()
        {
            return context.VehicleMakes.AsEnumerable();
        }
    }
}
