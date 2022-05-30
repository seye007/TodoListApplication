using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Data;
using ToDoList.Domain;

namespace ToDoList.BusinessLogic.Configuration
{
    public static class IdentityConfiguration
    {
        public static void ConfigurationIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(x =>
            {
                x.Password.RequireUppercase = true;
                x.Password.RequiredLength = 7;
                x.Password.RequireDigit = false;
                x.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<DbContext>()
                .AddDefaultTokenProviders();

        }
    }
}
