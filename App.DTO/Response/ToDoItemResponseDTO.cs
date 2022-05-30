using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.Response
{
    public class ToDoItemResponseDTO
    {
        public Guid id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid AssignBy { get; set; }
    }
}
