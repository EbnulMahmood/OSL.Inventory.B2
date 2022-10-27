namespace OSL.Inventory.B2.Repository.Migrations
{
    using OSL.Inventory.B2.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OSL.Inventory.B2.Repository.Data.InventoryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OSL.Inventory.B2.Repository.Data.InventoryDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var categories = new List<Category>
            {
                new Category { Name = "Electronics", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Fruits", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Groceries", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Beverage", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Cosmetics", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Est ullamcorper", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Nisi est", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Semper feugiat", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Commodo quis", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Metus vulputate", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Faucibus pulvinar", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Massa sed", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Posuere lorem", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Auctor urna", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Eget nunc", CreatedBy = 1, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
            };
            categories.ForEach(x => context.Categories.AddOrUpdate(c => c.Name, x));
            context.SaveChanges();
        }
    }
}
