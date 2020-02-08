namespace BRSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingValuetoBRSRequestModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_BRSRequest", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_BRSRequest", "Value");
        }
    }
}
