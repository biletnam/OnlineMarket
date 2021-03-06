﻿using AutoMapper;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.Models;

namespace OnlineMarket
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Resource, string>().ConstructUsing((resource) => resource.Title);
                cfg.CreateMap<DealType, string>().ConstructUsing((dealtype) => dealtype.Type);
                cfg.CreateMap<Deal, ArchiveViewModel>();
                cfg.CreateMap<Role, string>().ConstructUsing((role) => role.Title);
                cfg.CreateMap<User, UserViewModel>();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}