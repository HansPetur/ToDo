namespace ProjectToDoList.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ProjectToDoList.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectToDoList.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ProjectToDoList.Models.ApplicationDbContext context)
        {
            // This method will be called after migrating to the latest version.

            // Seed a user.
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var newUser = new ApplicationUser();
            newUser.UserName = "Monkey";
            var res = manager.Create(newUser, "123456");
            
            // Seed a list.
            var toDoLists = new ToDoList[]
            {
                new ToDoList{ Name="A to-do list", Owner=manager.FindByName("Monkey") }
            };

            // Seed some test events.
            var events = new ToDoItem[]
            {
                new ToDoItem{Description="Test", Owner=toDoLists[0]},
                new ToDoItem{Description="Things that need doing.", Owner=toDoLists[0]}
            };
            context.Events.AddOrUpdate(events);
            context.SaveChanges();


            // Call super class Seed fn.
            base.Seed(context);
        }
    }
}
