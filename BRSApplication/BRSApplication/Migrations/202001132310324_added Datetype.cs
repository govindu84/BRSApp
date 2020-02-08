namespace BRSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDatetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_BRSRequest", "ExpiryDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_BRSRequest", "ExpiryDate", c => c.DateTime());
        }
    }
}
