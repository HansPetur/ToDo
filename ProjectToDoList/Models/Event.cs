using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Models
{
    public class Event
    {
        [Required]
        [Key]
        [HiddenInput(DisplayValue=false)]
        public int ID{ get; set; }

        [Required]
        public string EventName { get; set; }

        public string Description { get; set; }

        public int something { get; set; }

        [Required]
        public DateTime Created { get; private set; }

        public DateTime Deadline { get; set; }

        public Event()
        {
            Created = DateTime.Now;
        }
    }
}