using ToDoList.DTO.Request;
using ToDoList.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Interface
{
    public interface IAuthenticationService
    {
        Task<Response<LoginUserResponseDTO>> LoginAsync(UserRequestDTO userRequest);
        Task<UserRegistrationResponseDTO> RegisterAsync(UserRegistrationRequestDTO registrationRequest);
    }
}

