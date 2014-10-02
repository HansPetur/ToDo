namespace ProjectToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "List_Id", "dbo.ToDoLists");
            DropForeignKey("dbo.ToDoLists", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "List_Id" });
            DropIndex("dbo.ToDoLists", new[] { "Owner_Id" });
            CreateTable(
                "dbo.ToDoItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        Owner_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToDoLists", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id);
            
            AlterColumn("dbo.ToDoLists", "Owner_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ToDoLists", "Owner_Id");
            AddForeignKey("dbo.ToDoLists", "Owner_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.ToDoLists", "OwnerID");
            DropTable("dbo.Events");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        List_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ToDoLists", "OwnerID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ToDoLists", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ToDoItems", "Owner_Id", "dbo.ToDoLists");
            DropIndex("dbo.ToDoLists", new[] { "Owner_Id" });
            DropIndex("dbo.ToDoItems", new[] { "Owner_Id" });
            AlterColumn("dbo.ToDoLists", "Owner_Id", c => c.String(maxLength: 128));
            DropTable("dbo.ToDoItems");
            CreateIndex("dbo.ToDoLists", "Owner_Id");
            CreateIndex("dbo.Events", "List_Id");
            AddForeignKey("dbo.ToDoLists", "Owner_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Events", "List_Id", "dbo.ToDoLists", "Id");
        }
    }
}
