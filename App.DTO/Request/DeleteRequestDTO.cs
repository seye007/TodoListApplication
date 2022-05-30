using System;
using System.ComponentModel.DataAnnotations;
using ToDoItem.Domain.Enums;

namespace ToDoList.DTO.Request
{
    public class DeleteRequestDTO
    {
        public Guid Id {get; set;}
        [Required(ErrorMessage = "Activity Details is required")]
        public string ActivityDetails { get; set; }
    }
}

