using System;
using ToDoItem.Domain.Enums;

namespace ToDoList.Domain
{
    public class ActivityLog : BaseEntity
    {
        public EntityType EntityType { get; set; }
        public Guid EntityTypeId { get; set; }
        public string ActivityDetails { get; set; }
    }
}
