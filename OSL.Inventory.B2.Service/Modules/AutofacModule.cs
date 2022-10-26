using Autofac;
using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Repository.Data;
using OSL.Inventory.B2.Repository.Data.Interfaces;
using OSL.Inventory.B2.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service.Modules
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // register dbcontext
            builder.Register(c => new InventoryDbContext(_connectionString)).
                             As<IInventoryDbContext>().InstancePerRequest();
            // register repositories
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerRequest();
            // register services

            base.Load(builder);
        }
    }
}
