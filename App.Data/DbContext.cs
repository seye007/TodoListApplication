using ToDoList.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Data
{
    public class DbContext : IdentityDbContext<User>
    {
        public DbContext (DbContextOptions<DbContext> options) : base(options)
        {

        }
        public DbSet<Domain.ToDoItem> ToDoItems;
        public DbSet<ActivityLog> ActivityLog { get; set; }
    }
}