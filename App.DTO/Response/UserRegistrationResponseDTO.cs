﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoItemm.Domain.Enums;

namespace ToDoList.DTO.Response
{
    public class UserRegistrationResponseDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public string OfficialPosition { get; set; }

    }
}
