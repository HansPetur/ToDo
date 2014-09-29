namespace ProjectToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated1404 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "EventName", c => c.String());
            DropColumn("dbo.Events", "Description");
            DropColumn("dbo.Events", "something");
            DropColumn("dbo.Events", "Deadline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "something", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "Description", c => c.String());
            AlterColumn("dbo.Events", "EventName", c => c.String(nullable: false));
        }
    }
}
