namespace WorldSkills.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "Full_name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Phone_number", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Phone_number", c => c.String());
            AlterColumn("dbo.Users", "Full_name", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
        }
    }
}
