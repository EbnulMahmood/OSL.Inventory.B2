namespace OSL.Inventory.B2.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasicUnitEnumToProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Description = c.String(maxLength: 500),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        EmailAddress = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 30),
                        Country = c.String(maxLength: 50),
                        City = c.String(maxLength: 80),
                        State = c.String(maxLength: 80),
                        ZipCode = c.String(maxLength: 20),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Description = c.String(maxLength: 500),
                        ImageUrl = c.String(maxLength: 255),
                        Limited = c.Boolean(nullable: false),
                        InStock = c.Int(nullable: false),
                        PricePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BasicUnit = c.Int(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.PurchaseDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        QuantityPurchased = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PricePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Long(nullable: false),
                        PurchaseId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PurchaseCode = c.String(maxLength: 255),
                        PurchaseAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseDate = c.DateTime(nullable: false),
                        PurchaseAmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaidTime = c.DateTime(nullable: false),
                        SupplierId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        EmailAddress = c.String(maxLength: 50),
                        PhoneNumber = c.String(maxLength: 30),
                        Country = c.String(maxLength: 50),
                        City = c.String(maxLength: 80),
                        State = c.String(maxLength: 80),
                        ZipCode = c.String(maxLength: 20),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SaleDetails",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        QuantitySold = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PricePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductId = c.Long(nullable: false),
                        SaleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SaleCode = c.String(maxLength: 255),
                        SaleAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleDate = c.DateTime(nullable: false),
                        SaleAmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaidTime = c.DateTime(nullable: false),
                        CustomerId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        Country = c.String(maxLength: 50),
                        City = c.String(maxLength: 80),
                        State = c.String(maxLength: 80),
                        ZipCode = c.String(maxLength: 20),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        CreatedBy = c.Long(nullable: false),
                        ModifiedBy = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SaleDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.PurchaseDetails", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.PurchaseDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.SaleDetails", new[] { "SaleId" });
            DropIndex("dbo.SaleDetails", new[] { "ProductId" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseDetails", new[] { "PurchaseId" });
            DropIndex("dbo.PurchaseDetails", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Users");
            DropTable("dbo.Sales");
            DropTable("dbo.SaleDetails");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchaseDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
        }
    }
}
