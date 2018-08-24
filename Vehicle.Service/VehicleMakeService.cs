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
   public class VehicleMakeService : IVehicleMakeService
    {
        VehicleContext context = new VehicleContext();


        //Add new VehicleMake
        public void Create(IVehicleMake VMake)
        {
            context.VehicleMakes.Add(AutoMapper.Mapper.Map<VehicleMake>(VMake));
            context.SaveChanges();
        }

        //Update already existing VehicleMake
        public void Update (IVehicleMake VMake)
        {

            context.Entry(VMake).State = EntityState.Modified;
            context.SaveChanges();
        }

        //Find VehicleMake by ID and delete it
        public void Delete(IVehicleMake VMake)
        {
           context.VehicleMakes.Remove(context.VehicleMakes.Where(x => x.ID == VMake.ID).FirstOrDefault());
            context.SaveChanges();
        }

        //Paged list of all VehicleMakes with sorting and filtering
        public IPagedList<IVehicleMake> GetAll(ISorting sort, IFiltering search, IPaging paging)
        {
            var list = context.VehicleMakes.AsEnumerable();


            //Search
            if(!String.IsNullOrEmpty(search.searchString))
            {
                list = list.Where(x => x.Name.ToUpper().StartsWith(search.searchString.ToUpper())
                                || x.Abrv.StartsWith(search.searchString));

            }


            //Sorting
            switch(sort.sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(x=> x.Name);
                    break;
                case "Abrv":
                    list = list.OrderBy(x => x.Abrv);
                    break;
                case "Abrv_desc":
                    list = list.OrderByDescending(x => x.Abrv);
                    break;
                default:
                    list = list.OrderBy(x=> x.Name);
                    break;
            }


            return list.ToPagedList(paging.page, paging.pageSize);
        }

        //Find one VehicleMake by ID
        public IVehicleMake FindByID (int? id)
        {
           return context.VehicleMakes.Where(x => x.ID == id).FirstOrDefault();
        }


    }
}
