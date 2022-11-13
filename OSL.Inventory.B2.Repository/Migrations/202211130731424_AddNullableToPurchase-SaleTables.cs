namespace OSL.Inventory.B2.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableToPurchaseSaleTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Purchases", "PurchaseAmountPaid", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Purchases", "AmountPaidTime", c => c.DateTime());
            AlterColumn("dbo.Sales", "SaleAmountPaid", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Sales", "AmountPaidTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "AmountPaidTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Sales", "SaleAmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Purchases", "AmountPaidTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Purchases", "PurchaseAmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
