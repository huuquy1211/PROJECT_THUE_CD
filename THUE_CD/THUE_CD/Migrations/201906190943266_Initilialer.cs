namespace THUE_CD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initilialer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id_Account = c.Int(nullable: false, identity: true),
                        AccountName = c.String(),
                        PassWord = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id_Account);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id_Customer = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Fine = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Customer);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id_Order = c.Int(nullable: false, identity: true),
                        Id_Customer = c.Int(nullable: false),
                        TotalRent = c.Double(nullable: false),
                        DateRent = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Order)
                .ForeignKey("dbo.Customers", t => t.Id_Customer, cascadeDelete: true)
                .Index(t => t.Id_Customer);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id_OrderDetail = c.Int(nullable: false, identity: true),
                        Id_Order = c.Int(nullable: false),
                        Id_Item = c.Int(nullable: false),
                        DateReturn = c.DateTime(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        LateFee = c.Double(nullable: false),
                        RentFee = c.Double(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id_OrderDetail)
                .ForeignKey("dbo.Items", t => t.Id_Item, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Id_Order, cascadeDelete: true)
                .Index(t => t.Id_Order)
                .Index(t => t.Id_Item);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id_Item = c.Int(nullable: false, identity: true),
                        Id_Title = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id_Item)
                .ForeignKey("dbo.Titles", t => t.Id_Title, cascadeDelete: true)
                .Index(t => t.Id_Title);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        Id_Title = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountOfItem = c.Int(nullable: false),
                        Id_TypeDisk = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_Title)
                .ForeignKey("dbo.TypeDisks", t => t.Id_TypeDisk, cascadeDelete: true)
                .Index(t => t.Id_TypeDisk);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Title = c.Int(nullable: false),
                        Id_Customer = c.Int(nullable: false),
                        Id_Item = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id_Customer, cascadeDelete: true)
                .ForeignKey("dbo.Titles", t => t.Id_Title, cascadeDelete: true)
                .Index(t => t.Id_Title)
                .Index(t => t.Id_Customer);
            
            CreateTable(
                "dbo.TypeDisks",
                c => new
                    {
                        Id_TypeDisk = c.Int(nullable: false, identity: true),
                        NameType = c.String(),
                        RentPrice = c.Double(nullable: false),
                        LateFee = c.Double(nullable: false),
                        MaxDate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_TypeDisk);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Id_Order", "dbo.Orders");
            DropForeignKey("dbo.Titles", "Id_TypeDisk", "dbo.TypeDisks");
            DropForeignKey("dbo.Reservations", "Id_Title", "dbo.Titles");
            DropForeignKey("dbo.Reservations", "Id_Customer", "dbo.Customers");
            DropForeignKey("dbo.Items", "Id_Title", "dbo.Titles");
            DropForeignKey("dbo.OrderDetails", "Id_Item", "dbo.Items");
            DropForeignKey("dbo.Orders", "Id_Customer", "dbo.Customers");
            DropIndex("dbo.Reservations", new[] { "Id_Customer" });
            DropIndex("dbo.Reservations", new[] { "Id_Title" });
            DropIndex("dbo.Titles", new[] { "Id_TypeDisk" });
            DropIndex("dbo.Items", new[] { "Id_Title" });
            DropIndex("dbo.OrderDetails", new[] { "Id_Item" });
            DropIndex("dbo.OrderDetails", new[] { "Id_Order" });
            DropIndex("dbo.Orders", new[] { "Id_Customer" });
            DropTable("dbo.TypeDisks");
            DropTable("dbo.Reservations");
            DropTable("dbo.Titles");
            DropTable("dbo.Items");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Accounts");
        }
    }
}
