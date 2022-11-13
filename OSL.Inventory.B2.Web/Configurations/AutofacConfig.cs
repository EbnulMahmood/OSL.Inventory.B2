using Autofac;
using Autofac.Integration.Mvc;
using OSL.Inventory.B2.Service.DependencyRegistry;
using System.Web.Mvc;

namespace OSL.Inventory.B2.Web.Configurations
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register our dependencies
            builder.RegisterModule(new AutofacModule());

            var container = builder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}