namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfitColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "Profit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "DeliveryPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "DeliveryPrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Sales", "Profit");
        }
    }
}
