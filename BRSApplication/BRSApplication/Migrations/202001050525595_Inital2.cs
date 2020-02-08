namespace BRSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_BRSRequest", "ApproveDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_BRSRequest", "ApproveDate", c => c.Int(nullable: false));
        }
    }
}
