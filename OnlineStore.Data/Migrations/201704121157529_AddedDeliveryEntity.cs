namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDeliveryEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDeliveries",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        DeliveryId = c.Int(nullable: false),
                        DeliveredQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.DeliveryId })
                .ForeignKey("dbo.Deliveries", t => t.DeliveryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.DeliveryId);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        DeliveryId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        TotalSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DeliveryId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: false)
                .Index(t => t.SupplierId);
            
            AddColumn("dbo.Products", "DeliveryPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDeliveries", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Deliveries", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.ProductDeliveries", "DeliveryId", "dbo.Deliveries");
            DropIndex("dbo.Deliveries", new[] { "SupplierId" });
            DropIndex("dbo.ProductDeliveries", new[] { "DeliveryId" });
            DropIndex("dbo.ProductDeliveries", new[] { "ProductId" });
            DropColumn("dbo.Products", "DeliveryPrice");
            DropTable("dbo.Deliveries");
            DropTable("dbo.ProductDeliveries");
        }
    }
}
