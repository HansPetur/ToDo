using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ProjectToDoList.Models
{
    public class ToDoList
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int OwnerID { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Event> Events;
    }
}