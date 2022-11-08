using OSL.Inventory.B2.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace OSL.Inventory.B2.Repository.Migrations
{
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
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 54000, BasicUnit = Entity.Enums.BasicUnit.Piece, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Desktop", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 500, PricePerUnit = 14000, BasicUnit = Entity.Enums.BasicUnit.Piece, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Samsung Mobile", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 20, PricePerUnit = 54030, BasicUnit = Entity.Enums.BasicUnit.Liter, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Velit euismod", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 52000, BasicUnit = Entity.Enums.BasicUnit.Liter, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Nibh praesent", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 54010, BasicUnit = Entity.Enums.BasicUnit.Liter, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Nunc aliquet", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 4000, BasicUnit = Entity.Enums.BasicUnit.Piece, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Metus aliquam", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 14000, BasicUnit = Entity.Enums.BasicUnit.Piece, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "A iaculis", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 24000, BasicUnit = Entity.Enums.BasicUnit.Kg, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Arcu vitae", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 5000, BasicUnit = Entity.Enums.BasicUnit.Kg, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Orci a scelerisque", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 43000, BasicUnit = Entity.Enums.BasicUnit.Kg, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
                new Product { Name = "Dolor magna", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = null, Limited = false, InStock = 200, PricePerUnit = 51000, BasicUnit = Entity.Enums.BasicUnit.Piece, CreatedBy = 1, CreatedAt = DateTime.Now, CategoryId = 1},
            };

            products.ForEach(x => context.Products.AddOrUpdate(c => c.Id, x));
            context.SaveChanges();

            var suppliers = new List<Supplier>
            {
                new Supplier { FirstName = "Rakib", LastName = "Khan", EmailAddress = "rakib@example.com", PhoneNumber = "019793979374", CreatedBy = 1, CreatedAt = DateTime.Now },
                new Supplier { FirstName = "Shakib", LastName = "Khan", EmailAddress = "shakib@example.com", PhoneNumber = "019793979374", CreatedBy = 1, CreatedAt = DateTime.Now },
                new Supplier { FirstName = "Arif", LastName = "Khan", EmailAddress = "arif@example.com", PhoneNumber = "019793979374", CreatedBy = 1, CreatedAt = DateTime.Now },
            };

            suppliers.ForEach(x => context.Suppliers.AddOrUpdate(c => c.Id, x));
            context.SaveChanges();

            var purchases = new List<Purchase>
            {
                new Purchase { SupplierId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "12345678834", PurchaseAmount = 20000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 20000, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "23498254798", PurchaseAmount = 10000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 30000, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "54645654477", PurchaseAmount = 30000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 50000, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "23423242444", PurchaseAmount = 40000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 60000, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "23424567756", PurchaseAmount = 50000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 60000, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "32342323334", PurchaseAmount = 60000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 70000, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "21324243435", PurchaseAmount = 70000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 780000, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "56756767678", PurchaseAmount = 30000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 25000, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "23423434545", PurchaseAmount = 30000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 20600, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 3, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "54656778886", PurchaseAmount = 60000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 20600, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 3, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "22423435345", PurchaseAmount = 70000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 20600, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 3, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "23137888887", PurchaseAmount = 250000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 26000, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "43356532233", PurchaseAmount = 20600, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 20060, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "45656567774", PurchaseAmount = 2700, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 20070, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "33535454545", PurchaseAmount = 60000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 20700, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "34577566756", PurchaseAmount = 28000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 20700, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "54656456456", PurchaseAmount = 28000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 20300, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "56767676322", PurchaseAmount = 23000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 203400, AmountPaidTime = DateTime.Now },
                new Purchase { SupplierId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, PurchaseCode = "54545676878", PurchaseAmount = 26000, PurchaseDate = DateTime.Now, PurchaseAmountPaid = 200300, AmountPaidTime = DateTime.Now },
            };

            purchases.ForEach(x => context.Purchases.AddOrUpdate(c => c.Id, x));
            context.SaveChanges();

            var purchaseDetails = new List<PurchaseDetail>
            {
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 1 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 2, PurchaseId = 1 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 3, PurchaseId = 1 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 4, PurchaseId = 1 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 5, PurchaseId = 1 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 2 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 3 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 4 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 5 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 6 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 7 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 8 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 8 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 10 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 11 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 12 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 13 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 14 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 15 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 16 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 17 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 18 },
                new PurchaseDetail { QuantityPurchased = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, PurchaseId = 19 },
            };

            purchaseDetails.ForEach(x => context.PurchaseDetails.AddOrUpdate(c => c.Id, x));
            context.SaveChanges();

            var customers = new List<Customer>
            {
                new Customer { FirstName = "Sofiq", LastName = "Ahmed", EmailAddress = "sofiq@example.com", PhoneNumber = "794872232234", CreatedBy = 1, CreatedAt = DateTime.Now },
                new Customer { FirstName = "Abid", LastName = "Ahmed", EmailAddress = "abid@example.com", PhoneNumber = "794872232234", CreatedBy = 1, CreatedAt = DateTime.Now },
                new Customer { FirstName = "Rafi", LastName = "Ahmed", EmailAddress = "rafi@example.com", PhoneNumber = "794872232234", CreatedBy = 1, CreatedAt = DateTime.Now },
            };

            customers.ForEach(x => context.Customers.AddOrUpdate(c => c.Id, x));
            context.SaveChanges();

            var sales = new List<Sale>
            {
                new Sale { CustomerId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "12345678834", SaleAmount = 20000, SaleDate = DateTime.Now, SaleAmountPaid = 20000, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "23498254798", SaleAmount = 10000, SaleDate = DateTime.Now, SaleAmountPaid = 30000, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "54645654477", SaleAmount = 30000, SaleDate = DateTime.Now, SaleAmountPaid = 50000, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "23423242444", SaleAmount = 40000, SaleDate = DateTime.Now, SaleAmountPaid = 60000, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "23424567756", SaleAmount = 50000, SaleDate = DateTime.Now, SaleAmountPaid = 60000, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "32342323334", SaleAmount = 60000, SaleDate = DateTime.Now, SaleAmountPaid = 70000, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "21324243435", SaleAmount = 70000, SaleDate = DateTime.Now, SaleAmountPaid = 780000, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "56756767678", SaleAmount = 30000, SaleDate = DateTime.Now, SaleAmountPaid = 25000, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "23423434545", SaleAmount = 30000, SaleDate = DateTime.Now, SaleAmountPaid = 20600, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 3, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "54656778886", SaleAmount = 60000, SaleDate = DateTime.Now, SaleAmountPaid = 20600, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 3, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "22423435345", SaleAmount = 70000, SaleDate = DateTime.Now, SaleAmountPaid = 20600, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 3, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "23137888887", SaleAmount = 250000, SaleDate = DateTime.Now, SaleAmountPaid = 26000, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "43356532233", SaleAmount = 20600, SaleDate = DateTime.Now, SaleAmountPaid = 20060, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "45656567774", SaleAmount = 2700, SaleDate = DateTime.Now, SaleAmountPaid = 20070, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "33535454545", SaleAmount = 60000, SaleDate = DateTime.Now, SaleAmountPaid = 20700, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "34577566756", SaleAmount = 28000, SaleDate = DateTime.Now, SaleAmountPaid = 20700, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 1, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "54656456456", SaleAmount = 28000, SaleDate = DateTime.Now, SaleAmountPaid = 20300, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "56767676322", SaleAmount = 23000, SaleDate = DateTime.Now, SaleAmountPaid = 203400, AmountPaidTime = DateTime.Now },
                new Sale { CustomerId = 2, CreatedBy = 1, CreatedAt = DateTime.Now, SaleCode = "54545676878", SaleAmount = 26000, SaleDate = DateTime.Now, SaleAmountPaid = 200300, AmountPaidTime = DateTime.Now },
            };

            sales.ForEach(x => context.Sales.AddOrUpdate(c => c.Id, x));
            context.SaveChanges();

            var saleDetails = new List<SaleDetail>
            {
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 1 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 2, SaleId = 1 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 3, SaleId = 1 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 4, SaleId = 1 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 5, SaleId = 1 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 2 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 3 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 4 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 5 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 6 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 7 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 8 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 8 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 10 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 11 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 12 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 13 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 14 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 15 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 16 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 17 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 18 },
                new SaleDetail { QuantitySold = 20, PricePerUnit = 20000, TotalPrice = 400000, ProductId = 1, SaleId = 19 },
            };

            saleDetails.ForEach(x => context.SaleDetails.AddOrUpdate(c => c.Id, x));
            context.SaveChanges();
        }
    }
}
