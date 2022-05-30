using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.Request
{
    public class ToDoItemsRequestDTO
    {
       public  string employeeEmail { get; set; }
       [Required(ErrorMessage = "Title is required")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Task description is required")]
        [DataType(DataType.Text)]
        public string TaskDescription { get; set; }
    }
}
