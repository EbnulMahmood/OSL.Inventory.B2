using Autofac;
using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Repository.Data;

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
            builder.RegisterType<InventoryDbContext>()
                .AsSelf()
                .InstancePerDependency();

            // register repositories
            builder.RegisterType<UnitOfWork>()
               .As<IUnitOfWork>()
               .InstancePerLifetimeScope();

            // register services
            builder.RegisterType<CategoryService>()
                .As<ICategoryService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ProductService>()
                .As<IProductService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
