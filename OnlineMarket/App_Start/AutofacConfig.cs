using Autofac;
using Autofac.Integration.WebApi;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.BusinessLogicLayer.Services;
using OnlineMarket.DataAccessLayer;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Repositories;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace OnlineMarket.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<OnlineMarketContext>()
                   .InstancePerRequest();

            builder.RegisterType<UserRepository>()
                .As<IRepository<User>>()
                .InstancePerRequest();

            builder.RegisterType<DealRepository>()
               .As<IRepository<Deal>>()
               .InstancePerRequest();

            builder.RegisterType<ResourceRepository>()
               .As<IRepository<Resource>>()
               .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .InstancePerRequest();

            builder.RegisterType<MembershipService>()
                .As<IMembershipService>()
                .InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();

            return Container;
        }
    }
}