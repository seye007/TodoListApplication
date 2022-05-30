using ToDoList.DTO.Response;
using AutoMapper;
using ToDoList.Domain;
using ToDoList.DTO.Request;
using ToDoItems.DTO.Response;

namespace ToDoList.DTO.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRegistrationRequestDTO>().ReverseMap();
            CreateMap<Domain.ToDoItem, ToDoItemsRequestDTO>().ReverseMap();
            CreateMap<Domain.ToDoItem, ToDoItemResponseDTO>().ReverseMap();
            CreateMap<Domain.ToDoItem, UpdateToDoItemRequestDTO>().ReverseMap();
            CreateMap<UpdateToDoItemResponseDTO, Domain.ToDoItem>().ReverseMap();
            CreateMap<ActivityLog, DeleteRequestDTO>().ReverseMap();
            CreateMap<UserRegistrationResponseDTO, User>().ReverseMap();
            CreateMap<LoginUserResponseDTO, User>().ReverseMap();
            CreateMap<CompletedTaskResponseDTO, Domain.ToDoItem>().ReverseMap();
        }
    }
}
