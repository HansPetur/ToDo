using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjectToDoList.Models;

namespace ProjectToDoList.Models
{
    public class ToDoList
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<ToDoItem> ToDoItems { get; set; }

        [Required]
        public ApplicationUser Owner { get; set; }

        public ToDoList()
        {
            ToDoItems = new List<ToDoItem>();
        }
    }
}