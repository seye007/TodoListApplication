using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoItems.DTO.Response;
using ToDoList.BusinessLogic.Interface;
using ToDoList.DTO.Request;
using ToDoList.DTO.Response;

namespace TodoListApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemService _toDoItemService;
        public ToDoItemController(IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
        }

        [HttpPost]
        [Route(nameof(AssignTask))]
        [Authorize(Policy = "RequireAdminOnly")]
        public async Task<IActionResult> AssignTask(ToDoItemsRequestDTO ToDoItemRequest)
        {
            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            try
            {
                Response <ToDoItemResponseDTO> response = await _toDoItemService.AssignTaskAsync(ToDoItemRequest, userId);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured we are working on it");
            }
        }

        [HttpPatch]
        [Route(nameof(EditAssignedTask))]
        [Authorize(Policy = "RequireAdminOnly")]
        public async Task<IActionResult> EditAssignedTask(UpdateToDoItemRequestDTO ToDoItemRequest)
        {
            //var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            try
            {
                Response<UpdateToDoItemResponseDTO> response = await _toDoItemService.UpdateAssignedTaskAsync(ToDoItemRequest);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured we are working on it");
            }
        }

        [HttpPatch]
        [Route(nameof(DeleteAssignedTask))]
        [Authorize(Policy = "RequireAdminOnly")]
        public async Task<IActionResult> DeleteAssignedTask(DeleteRequestDTO ToDoItemRequest)
        {
            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            try
            {
                Response<string> response = await _toDoItemService.DeleteAssignedTaskByIdAsync(ToDoItemRequest, userId);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured we are working on it");
            }
        }

        [HttpPatch]
        [Route(nameof(CompletedTask))]
        [Authorize(Policy = "RequireRegularOnly")]
        public async Task<IActionResult> CompletedTask(string taskId)
        {
            var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            try
            {
                Response<CompletedTaskResponseDTO> response =  await _toDoItemService.CompletedTask(taskId);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured we are working on it");
            }
        }
    }
}
