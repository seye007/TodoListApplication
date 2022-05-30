using ToDoList.BusinessLogic.Interface;
using ToDoList.Domain;
using ToDoList.DTO.Response;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data.GenericRepo;
using ToDoList.DTO.Request;
using ToDoItems.DTO.Response;
using ToDoItem.Domain.Enums;
namespace ToDoList.BusinessLogic.Implementation
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepo<Domain.ToDoItem> _toDoItemGenericRepo;
        private readonly IGenericRepo<ActivityLog> _activityLogGenericRepo;
        private readonly IMapper _mapper;
        public ToDoItemService(UserManager<User> userManager,
            IGenericRepo<Domain.ToDoItem> toDoItemGenericRepo,
            IGenericRepo<ActivityLog> activityLogGenericRepo, IMapper mapper)
        {
            _userManager = userManager;
            _toDoItemGenericRepo = toDoItemGenericRepo;
            _activityLogGenericRepo = activityLogGenericRepo;
            _mapper = mapper;
        }
        public async Task<Response<ToDoItemResponseDTO>> AssignTaskAsync(ToDoItemsRequestDTO ToDoItemRequest, string adminId)
        {
            var employee = await _userManager.FindByEmailAsync(ToDoItemRequest.employeeEmail);
            var admin = await _userManager.FindByIdAsync(adminId);
            if (employee == null)
            {
                throw new ArgumentException($"User not found");
            }
            if (admin == employee)
            {
                throw new ArgumentException($"You cannot assign task to youself");
            }

            var task = _mapper.Map<Domain.ToDoItem>(ToDoItemRequest);
            task.CreatedOn = DateTime.Now;
            task.UserId = Guid.Parse(employee.Id);
            task.AssignBy = Guid.Parse(admin.Id);

            Response<ToDoItemResponseDTO> response = new Response<ToDoItemResponseDTO>();
            if (await _toDoItemGenericRepo.InsertAsync(task))
            {
                ToDoItemResponseDTO responseDTO = _mapper.Map<ToDoItemResponseDTO>(task);
                {
                    response.Data = responseDTO;
                    response.Message = "Task Assigned";
                    response.Success = true;
                };
            }

            response.Errors = "Failed to assign task";
            return response;
        }

        public async Task<Response<UpdateToDoItemResponseDTO>> UpdateAssignedTaskAsync(UpdateToDoItemRequestDTO updateToDoItem)
        {
            var isValidGuid = Guid.TryParse(updateToDoItem.TaskId, out Guid taskId);

            if (!isValidGuid)
                throw new ArgumentException($"taskId {updateToDoItem.TaskId} is invalid");

            using (var transaction = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
            {
                var toDoItems = await _toDoItemGenericRepo.GetByIdAysnc(taskId);
                Domain.ToDoItem toDoItem = _mapper.Map<Domain.ToDoItem>(updateToDoItem);
                toDoItem.ModifiedOn = DateTime.Now;
                await _toDoItemGenericRepo.UpdateAsync(toDoItem);

                var response = new Response<UpdateToDoItemResponseDTO>()
                {
                    Errors = null,
                    Message = "Successful",
                    Success = true,
                    Data = _mapper.Map<UpdateToDoItemResponseDTO>(toDoItem)
                };

                transaction.Complete();

                return response;
            }
        }

        public async Task<Response<string>> DeleteAssignedTaskByIdAsync(DeleteRequestDTO assignedtask, string adminId)
        {
            
                var admin = _userManager.FindByIdAsync(adminId);
                var assignedTask = _toDoItemGenericRepo.Table
                                    .Where(d => d.Id == assignedtask.Id)
                                    .FirstOrDefault();

                if (assignedTask.AssignBy == Guid.Parse(adminId))
                {
                    assignedTask.IsDeprecated = true;
                    assignedTask.DeletedBy = Guid.Parse(adminId);

                ActivityLog activityLog = new ActivityLog()
                {
                    EntityTypeId = assignedTask.Id,
                    EntityType = EntityType.Admin,
                    ActivityDetails = assignedtask.ActivityDetails,
                    CreatedOn = DateTime.Now,
                };
                await _toDoItemGenericRepo.UpdateAsync(assignedTask);
                await _activityLogGenericRepo.InsertAsync(activityLog);
                return new Response<string>()
                {
                    Message = $"Task assigned to {assignedTask.User.FirstName + assignedTask.User.LastName} has been successfully deleted",
                    Success = true
                };
            }

            throw new ArgumentException($"Assined task with {assignedtask} not found");
        }

        public async Task<Response<CompletedTaskResponseDTO>> CompletedTask(string id)
        {
            var isValidGuid = Guid.TryParse(id, out Guid taskId);

            if (!isValidGuid)
                throw new ArgumentException($"userId {taskId} is invalid");
            var assignedTask = _toDoItemGenericRepo.Table
                                    .Where(d => d.Id == taskId)
                                    .FirstOrDefault();
            assignedTask.IsCompleted = true;
            await _toDoItemGenericRepo.UpdateAsync(assignedTask);
            var response = new Response<CompletedTaskResponseDTO>()
            {
                Errors = null,
                Message = "Successful",
                Success = true,
                Data =  _mapper.Map<CompletedTaskResponseDTO>(assignedTask)
            };
            return response;
        }
    }
}

