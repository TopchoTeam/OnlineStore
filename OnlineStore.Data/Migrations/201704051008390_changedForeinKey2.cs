namespace OnlineStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedForeinKey2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "UserId");
            DropColumn("dbo.Users", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AccountId", c => c.Int(nullable: false));
            AddColumn("dbo.Accounts", "UserId", c => c.Int());
        }
    }
}
