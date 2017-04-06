namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 15),
                        FirstName = c.String(),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Accounts", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TotalSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDelivered = c.Boolean(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProductSales",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        SaleId = c.Int(nullable: false),
                        OrderedQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.SaleId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Unit = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProductSales", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Products", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.ProductSales", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Users", "UserId", "dbo.Accounts");
            DropIndex("dbo.Products", new[] { "SupplierId" });
            DropIndex("dbo.ProductSales", new[] { "SaleId" });
            DropIndex("dbo.ProductSales", new[] { "ProductId" });
            DropIndex("dbo.Sales", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "UserId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Products");
            DropTable("dbo.ProductSales");
            DropTable("dbo.Sales");
            DropTable("dbo.Users");
            DropTable("dbo.Accounts");
        }
    }
}
