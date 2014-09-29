namespace ProjectToDoList.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ToDoList.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectToDoList.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProjectToDoList.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            
                //context.People.AddOrUpdate(
                //  p => p.FullName,
                //  new Person { FullName = "Andrew Peters" },
                //  new Person { FullName = "Brice Lambson" },
                //  new Person { FullName = "Rowan Miller" }
                //);
            
            var events = new Event[]
            {
                new Event{EventName="Test"},
                new Event{EventName="Test2"}
            };
            context.Events.AddOrUpdate(events);
            context.SaveChanges();
        }
    }
}
