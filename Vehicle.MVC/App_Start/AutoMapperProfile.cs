using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vehicle.Models;
using Vehicle.Models.Common;
using Vehicle.MVC.ViewModels;

namespace Vehicle.MVC.App_Start
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IVehicleMake, VehicleMake>().ReverseMap();
            CreateMap<IVehicleModel, VehicleModel>().ReverseMap();

            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();

        }
    }
}