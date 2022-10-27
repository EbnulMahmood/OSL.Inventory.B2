using Autofac;
using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Repository.Data.Interfaces;
using OSL.Inventory.B2.Repository.Interfaces;
using OSL.Inventory.B2.Service.Interfaces;

namespace OSL.Inventory.B2.Service.DependencyRegistry
{
    public class AutofacModule : Module
    {
        public AutofacModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            // register dbcontext
            builder.Register(c => new InventoryDbContext()).
                             As<IInventoryDbContext>().InstancePerRequest();
            // register repositories
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerRequest();
            // register services
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
