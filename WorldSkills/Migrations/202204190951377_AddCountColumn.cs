namespace WorldSkills.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductOrders", "Count", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
        }
    }
}
