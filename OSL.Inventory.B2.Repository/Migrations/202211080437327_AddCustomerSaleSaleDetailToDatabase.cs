namespace OSL.Inventory.B2.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerSaleSaleDetailToDatabase : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.SaleDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.SaleDetails", new[] { "SaleId" });
            DropIndex("dbo.SaleDetails", new[] { "ProductId" });
            DropTable("dbo.Sales");
            DropTable("dbo.SaleDetails");
            DropTable("dbo.Customers");
        }
    }
}
