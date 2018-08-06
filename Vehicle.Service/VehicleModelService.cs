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

        public IList<IVehicleModel> GetAll()
        {
            return AutoMapper.Mapper.Map<IList<IVehicleModel>>(context.VehicleModels.ToList());
        }

        public IVehicleModel FindByID(int? id)
        {
            return context.VehicleModels.Where(x => x.ID == id).FirstOrDefault();

        }
    }
}
