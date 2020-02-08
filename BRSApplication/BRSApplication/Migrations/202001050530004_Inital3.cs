namespace BRSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_BRSRequest", "ApproveDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_BRSRequest", "ApproveDate", c => c.DateTime(nullable: false));
        }
    }
}
