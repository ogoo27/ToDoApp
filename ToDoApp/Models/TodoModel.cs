using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApp.Models
{
    public class TodoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description{ get; set; }
        
        public DateTime Date{ get; set; } = DateTime.Now;
        [Required]
        public string Status { get; set; }
    }
}