using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Authentication;
namespace ToDoList.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {

            }

            public DbSet<ToDoItemModel> ToDoItems { get; set; }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                builder.Entity<ToDoItemModel>(entity =>
                {
                    entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100);

                    entity.Property(e => e.ItemDescription)
                    .IsRequired()
                    .HasMaxLength(100);

                    entity.Property(e => e.ItemStatus)
                    .IsRequired()
                    .HasMaxLength(1);
                });

                base.OnModelCreating(builder);
            }
        }
    }

