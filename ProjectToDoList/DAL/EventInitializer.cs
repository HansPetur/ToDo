using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public class EventInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ToDoContext>
    {
        protected override void Seed(ToDoContext context)
        {
            var events = new List<Event>
            {
            new Event{EventName="Test"},
            new Event{EventName="Test2"}
            };
            events.ForEach(s => context.Events.Add(s));
            context.SaveChanges();
        }
    }
}