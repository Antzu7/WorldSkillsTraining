namespace WorldSkills.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCollectionOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "OrderId", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "OrderId" });
            CreateTable(
                "dbo.ProductOrders",
                c => new
                {
                    Product_Id = c.Int(nullable: false),
                    Order_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Product_Id, t.Order_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Order_Id);
            
            DropColumn("dbo.Products", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "OrderId", c => c.Int());
            DropForeignKey("dbo.ProductOrders", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.ProductOrders", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductOrders", new[] { "Order_Id" });
            DropIndex("dbo.ProductOrders", new[] { "Product_Id" });
            DropTable("dbo.ProductOrders");
            CreateIndex("dbo.Products", "OrderId");
            AddForeignKey("dbo.Products", "OrderId", "dbo.Orders", "Id");
        }
    }
}
