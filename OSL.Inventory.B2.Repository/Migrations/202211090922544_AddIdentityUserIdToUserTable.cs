namespace OSL.Inventory.B2.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdentityUserIdToUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IdentityUserId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IdentityUserId");
        }
    }
}
