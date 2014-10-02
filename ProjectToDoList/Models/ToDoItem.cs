using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectToDoList.Models
{
    public class ToDoItem
    {
        [Required]
        [Key]
        [HiddenInput(DisplayValue=false)]
        public int Id{ get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime Created { get; private set; }

        [Required]
        public bool IsDone { get; set; }

        [Required]
        public ToDoList Owner{ get; set; }

        public ToDoItem()
        {
            Created = DateTime.Now;
            IsDone = false;
        }
    }
}