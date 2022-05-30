using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDoList.BusinessLogic.Interface;
using ToDoList.DTO.Request;

namespace TodoListApplication.Controllers
{

    [Route("api/v1/[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationServices;
        public AccountController(IAuthenticationService authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        [HttpPost("signUp")]
        public async Task<ActionResult> CreateUserAsync(UserRegistrationRequestDTO customerRequest)
        {
            try
            {
                if (!TryValidateModel(customerRequest))
                {
                    return BadRequest();
                }
                var response = await _authenticationServices.RegisterAsync(customerRequest);
                if (response != null)
                {
                    return Ok(response);
                }
                return BadRequest();
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured we are working on it");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserRequestDTO userRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _authenticationServices.LoginAsync(userRequest);
                if (!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }


            catch (AccessViolationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured we are working on it");
            }
        }
    }
}