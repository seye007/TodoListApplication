using ToDoList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Interface
{
    public interface ITokenGenerator
    {
        public Task<string> GenerateTokenAsync(User user);
    }
}
