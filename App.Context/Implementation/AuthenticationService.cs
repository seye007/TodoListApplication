using ToDoList.BusinessLogic.Interface;
using ToDoList.Domain;
using ToDoList.DTO.Request;
using ToDoList.DTO.Response;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.BusinessLogic.Implementation
{
    #region Constructor
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private IMapper _mapper;
        public AuthenticationService(UserManager<User> userManager, ITokenGenerator tokenGenerator, IMapper mapper)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }
        #endregion

        #region Implimentation
        public async Task<Response<LoginUserResponseDTO>> LoginAsync(UserRequestDTO userRequest)
        {
            var user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, userRequest.Password))
                {
                    var response = new Response<LoginUserResponseDTO>()
                    {
                        Data = new LoginUserResponseDTO()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Token = await _tokenGenerator.GenerateTokenAsync(user)
                        }
                    };
                    response.Message = "Login Successful";
                    response.Success = true;
                    return response;
                }
                throw new AccessViolationException("Invalid credentials");
            }
            throw new AccessViolationException("Invalid Credntials");
        }

        public async Task<UserRegistrationResponseDTO> RegisterAsync(UserRegistrationRequestDTO registrationRequest)
        {
            User user = _mapper.Map<User>(registrationRequest);
            user.UserName = registrationRequest.Email;
            user.CreatedOn = DateTime.Now;
            IdentityResult result = await _userManager.CreateAsync(user, registrationRequest.Password);
            await _userManager.AddToRoleAsync(user, "Regular");
            if (result.Succeeded)
            {
                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var userResponse = _mapper.Map<UserRegistrationResponseDTO>(user);
                return userResponse;

            }
            string errors = result.Errors.Aggregate(string.Empty, (current, error) => current + (error.Description + Environment.NewLine));
            throw new ArgumentException(errors);
        }

        #endregion
    }
}
