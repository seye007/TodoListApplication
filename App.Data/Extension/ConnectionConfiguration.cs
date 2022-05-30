using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Data.Extension
{
    public static class ConnectionConfiguration
    {
        public static void RegisterDBContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DbContext>(options => options.UseNpgsql(config.GetConnectionString("DefaultConnection"),
            getAssembly => getAssembly.MigrationsAssembly(typeof(DbContext).Assembly.FullName)
            ));
        }
    }
}
