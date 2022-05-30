using System;

namespace ToDoList.Domain
{
    public class ToDoItem : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string TaskDescription { get; set; }
        public Guid AssignBy { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeprecated { get; set; }
        public Guid DeletedBy { get; set; } 
        public User User { get; set; }
    }
}