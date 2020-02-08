namespace BRSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingValueTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_Value",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValueName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbl_Value");
        }
    }
}
