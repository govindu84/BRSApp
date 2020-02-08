namespace BRSApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_BRSRequest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestedAlias = c.String(maxLength: 50),
                        RequestedDate = c.DateTime(nullable: false),
                        EnvironmentId = c.Int(nullable: false),
                        GeosId = c.String(),
                        SlotId = c.Int(nullable: false),
                        BuildId = c.Int(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        Comments = c.String(maxLength: 200),
                        FeatureAreaId = c.Int(nullable: false),
                        FeatureNameId = c.Int(nullable: false),
                        SettingId = c.Int(nullable: false),
                        ApproverAlias = c.String(maxLength: 50),
                        ApproveDate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Build", t => t.BuildId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Environment", t => t.EnvironmentId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_FeatureArea", t => t.FeatureAreaId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_FeatureName", t => t.FeatureNameId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Setting", t => t.SettingId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Slot", t => t.SlotId, cascadeDelete: true)
                .Index(t => t.EnvironmentId)
                .Index(t => t.SlotId)
                .Index(t => t.BuildId)
                .Index(t => t.FeatureAreaId)
                .Index(t => t.FeatureNameId)
                .Index(t => t.SettingId);
            
            CreateTable(
                "dbo.tbl_Build",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuildNumber = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_EnvGoBuildBrsMap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnvironmentId = c.Int(nullable: false),
                        GeoId = c.Int(nullable: false),
                        BuildId = c.Int(nullable: false),
                        BrsRequestId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_BRSRequest", t => t.BrsRequestId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Build", t => t.BuildId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Geos", t => t.GeoId, cascadeDelete: false)
                .ForeignKey("dbo.tbl_Environment", t => t.EnvironmentId, cascadeDelete: false)
                .Index(t => t.EnvironmentId)
                .Index(t => t.GeoId)
                .Index(t => t.BuildId)
                .Index(t => t.BrsRequestId);
            
            CreateTable(
                "dbo.tbl_Environment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnvironmentName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_EnvGeoMap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnvironmentId = c.Int(nullable: false),
                        GeoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Environment", t => t.EnvironmentId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Geos", t => t.GeoId, cascadeDelete: true)
                .Index(t => t.EnvironmentId)
                .Index(t => t.GeoId);
            
            CreateTable(
                "dbo.tbl_Geos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeoName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_FeatureArea",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureArea = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_FeatureName",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeatureName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_Setting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SettingValue = c.String(),
                        LastUpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_Slot",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Slot = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tbl_BRSRequest", "SlotId", "dbo.tbl_Slot");
            DropForeignKey("dbo.tbl_BRSRequest", "SettingId", "dbo.tbl_Setting");
            DropForeignKey("dbo.tbl_BRSRequest", "FeatureNameId", "dbo.tbl_FeatureName");
            DropForeignKey("dbo.tbl_BRSRequest", "FeatureAreaId", "dbo.tbl_FeatureArea");
            DropForeignKey("dbo.tbl_EnvGoBuildBrsMap", "EnvironmentId", "dbo.tbl_Environment");
            DropForeignKey("dbo.tbl_EnvGoBuildBrsMap", "GeoId", "dbo.tbl_Geos");
            DropForeignKey("dbo.tbl_EnvGeoMap", "GeoId", "dbo.tbl_Geos");
            DropForeignKey("dbo.tbl_EnvGeoMap", "EnvironmentId", "dbo.tbl_Environment");
            DropForeignKey("dbo.tbl_BRSRequest", "EnvironmentId", "dbo.tbl_Environment");
            DropForeignKey("dbo.tbl_EnvGoBuildBrsMap", "BuildId", "dbo.tbl_Build");
            DropForeignKey("dbo.tbl_EnvGoBuildBrsMap", "BrsRequestId", "dbo.tbl_BRSRequest");
            DropForeignKey("dbo.tbl_BRSRequest", "BuildId", "dbo.tbl_Build");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.tbl_EnvGeoMap", new[] { "GeoId" });
            DropIndex("dbo.tbl_EnvGeoMap", new[] { "EnvironmentId" });
            DropIndex("dbo.tbl_EnvGoBuildBrsMap", new[] { "BrsRequestId" });
            DropIndex("dbo.tbl_EnvGoBuildBrsMap", new[] { "BuildId" });
            DropIndex("dbo.tbl_EnvGoBuildBrsMap", new[] { "GeoId" });
            DropIndex("dbo.tbl_EnvGoBuildBrsMap", new[] { "EnvironmentId" });
            DropIndex("dbo.tbl_BRSRequest", new[] { "SettingId" });
            DropIndex("dbo.tbl_BRSRequest", new[] { "FeatureNameId" });
            DropIndex("dbo.tbl_BRSRequest", new[] { "FeatureAreaId" });
            DropIndex("dbo.tbl_BRSRequest", new[] { "BuildId" });
            DropIndex("dbo.tbl_BRSRequest", new[] { "SlotId" });
            DropIndex("dbo.tbl_BRSRequest", new[] { "EnvironmentId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.tbl_Slot");
            DropTable("dbo.tbl_Setting");
            DropTable("dbo.tbl_FeatureName");
            DropTable("dbo.tbl_FeatureArea");
            DropTable("dbo.tbl_Geos");
            DropTable("dbo.tbl_EnvGeoMap");
            DropTable("dbo.tbl_Environment");
            DropTable("dbo.tbl_EnvGoBuildBrsMap");
            DropTable("dbo.tbl_Build");
            DropTable("dbo.tbl_BRSRequest");
        }
    }
}
