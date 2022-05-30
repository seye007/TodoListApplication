using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoItems.DTO.Response
{
    public class UpdateToDoItemResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
