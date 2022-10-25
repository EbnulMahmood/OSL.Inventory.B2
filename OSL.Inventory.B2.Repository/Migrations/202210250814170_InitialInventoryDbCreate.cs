namespace OSL.Inventory.B2.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialInventoryDbCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        CreatedBy = c.Long(),
                        ModifiedBy = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        AspNetUserId = c.Long(),
                        CreatedBy = c.Long(),
                        ModifiedBy = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
        }
    }
}
