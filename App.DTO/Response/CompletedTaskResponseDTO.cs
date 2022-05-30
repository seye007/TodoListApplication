using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DTO.Response
{
    public class CompletedTaskResponseDTO
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public Guid AssignBy { get; set; }
        public bool IsCompleted { get; set; }
    }
}
