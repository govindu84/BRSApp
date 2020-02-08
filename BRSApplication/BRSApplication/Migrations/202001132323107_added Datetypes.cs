namespace BRSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDatetypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_BRSRequest", "RequestedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_BRSRequest", "RequestedDate", c => c.DateTime(nullable: false));
        }
    }
}
