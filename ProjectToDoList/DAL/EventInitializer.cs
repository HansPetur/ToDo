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
            new Event{EventName="Test", Description="This is a test", Deadline=DateTime.Parse("2014-09-26")},
            new Event{EventName="Test2", Description="Is it working?", Deadline=DateTime.Parse("2014-09-26")}
            };
            events.ForEach(s => context.Events.Add(s));
            context.SaveChanges();
        }
    }
}