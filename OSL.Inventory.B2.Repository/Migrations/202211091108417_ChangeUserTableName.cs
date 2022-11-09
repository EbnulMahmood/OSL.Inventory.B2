namespace OSL.Inventory.B2.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "InventoryUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.InventoryUsers", newName: "Users");
        }
    }
}
