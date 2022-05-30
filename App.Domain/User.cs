using ToDoItem.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using ToDoItemm.Domain.Enums;

namespace ToDoList.Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeprecated { get; set; }
        public string Department { get; set; }
        public string OfficialPosition { get; set; }
        public ICollection<ToDoItem> ToDoItems { get; set; }
        
    }
}