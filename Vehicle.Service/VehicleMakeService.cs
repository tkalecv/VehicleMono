using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Common;
using Vehicle.Service.Models;

namespace Vehicle.Service
{
   public class VehicleMakeService : IVehicleMakeService
    {
        VehicleContext context = new VehicleContext();


        VehicleMake klkl = new VehicleMake();

        public void Create(IVehicleMake vMake)
        {
            context.VehicleMakes.Add(AutoMapper.Mapper.Map<VehicleMake>(vMake));
            context.SaveChanges();
        }

        public void Update (IVehicleMake vMake)
        {

            context.Entry(vMake).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(IVehicleMake vMake)
        {
           context.VehicleMakes.Remove(context.VehicleMakes.Where(x => x.ID == vMake.ID).FirstOrDefault());
            context.SaveChanges();
        }

        public IList<IVehicleMake> GetAll()
        {
            return AutoMapper.Mapper.Map<IList<IVehicleMake>>(context.VehicleMakes.ToList());
        }

        public IVehicleMake FindByID (int? id)
        {
           return context.VehicleMakes.Where(x => x.ID == id).FirstOrDefault();
        }

    }
}
