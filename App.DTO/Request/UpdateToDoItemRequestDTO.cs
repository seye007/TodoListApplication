using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.Request
{
    public class UpdateToDoItemRequestDTO
    {
        [Required(ErrorMessage = "Assined task id is required")]
        public string TaskId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Task description is required")]
        [DataType(DataType.Text)]
        public string TaskDescription { get; set; }

    }
}
