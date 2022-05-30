using Microsoft.Extensions.DependencyInjection;
using ToDoList.BusinessLogic.Implementation;
using ToDoList.BusinessLogic.Interface;
using ToDoList.Data.GenericRepo;

namespace ToDoList.BusinessLogic.Configuration
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IToDoItemService, ToDoItemService>();
        }
    }
}
