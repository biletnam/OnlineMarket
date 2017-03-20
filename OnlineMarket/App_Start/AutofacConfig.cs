using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using log4net;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using OnlineMarket.BusinessLogicLayer.Interfaces;
using OnlineMarket.BusinessLogicLayer.Services;
using OnlineMarket.DataAccessLayer;
using OnlineMarket.DataAccessLayer.Entities;
using OnlineMarket.DataAccessLayer.Interfaces;
using OnlineMarket.DataAccessLayer.Repositories;
using OnlineMarket.Hubs;
using OnlineMarket.Utilities.Interfaces;
using OnlineMarket.Utilities.Servicies;

namespace OnlineMarket.App_Start
{
    public class AutofacConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config, IDependencyResolver dependencyResolver)
        {
            Initialize(config, RegisterServices(new ContainerBuilder(), dependencyResolver));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder, IDependencyResolver dependencyResolver)
        {
            builder.Register(i => dependencyResolver.Resolve<IConnectionManager>().GetHubContext<AppHub>())
                .ExternallyOwned();

            builder.Register(c => LogManager.GetLogger(typeof (object))).As<ILog>();

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

            builder.RegisterType<UserResourcesRepository>()
                .As<IRepository<UserResources>>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>()
                .InstancePerRequest();

            builder.RegisterType<SendEmailService>()
                .As<ISendEmailService>()
                .WithParameter("email", ConfigurationManager.AppSettings["email"])
                .WithParameter("password", ConfigurationManager.AppSettings["password"])
                .InstancePerRequest();

            builder.RegisterType<MembershipService>()
                .As<IMembershipService>()
                .InstancePerRequest();

            builder.RegisterType<ResourceService>()
                .As<IResourceService>()
                .InstancePerRequest();

            builder.RegisterType<DealService>()
                .As<IDealService>()
                .InstancePerRequest();

            builder.RegisterType<UserResourcesService>()
                .As<IUserResourcesService>()
                .InstancePerRequest();

            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            Container = builder.Build();

            return Container;
        }
    }
}