namespace OSL.Inventory.B2.Repository.Migrations
{
    using OSL.Inventory.B2.Entity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.InventoryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.InventoryDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var categories = new List<Category>
            {
                new Category { Name = "Electronics", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Fruits", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Groceries", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Beverage", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Cosmetics", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Est ullamcorper", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Nisi est", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Semper feugiat", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Commodo quis", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Metus vulputate", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Faucibus pulvinar", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Massa sed", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Posuere lorem", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Auctor urna", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
                new Category { Name = "Eget nunc", CreatedBy = 1, CreatedAt = DateTime.Now, Description = "Sit amet commodo nulla facilisi nullam vehicula ipsum a arcu. Sit amet consectetur adipiscing elit. Ut etiam sit amet nisl purus in mollis." },
            };
            categories.ForEach(x => context.Categories.AddOrUpdate(c => c.Name, x));
            context.SaveChanges();

            var products = new List<Product>
            {
                new Product { Name = "Laptop", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 54000, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Desktop", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 500, PricePerUnit = 14000, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Samsung Mobile", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 20, PricePerUnit = 54030, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Velit euismod", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 52000, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Nibh praesent", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 54010, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Nunc aliquet", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 4000, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Metus aliquam", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 14000, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "A iaculis", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 24000, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Arcu vitae", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 5000, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Orci a scelerisque", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 43000, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Dolor magna", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 51000, BasicUnit = "piece", CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
            };

            products.ForEach(x => context.Products.AddOrUpdate(c => c.Id, x));
            context.SaveChanges();
        }
    }
}
