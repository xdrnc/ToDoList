using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapi.Models
{
    public class ToDoItemViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
    }
}