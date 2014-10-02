using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Event
    {
        [Required]
        [Key]
        [HiddenInput(DisplayValue=false)]
        public int ID{ get; set; }
        [Required(ErrorMessage = "Cannot be empty!")]       
        public string EventName { get; set; }
        [Required]
        public DateTime Created { get; private set; }

        public Event()
        {
            Created = DateTime.Now;
        }
    }
}