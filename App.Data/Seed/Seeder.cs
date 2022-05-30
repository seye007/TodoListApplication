using ToDoList.Domain;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ToDoList.Data.Seed
{
    public class Seeder
    {
        public static async Task SeedData(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, DbContext dbContext)
        {
            var baseDir = Directory.GetCurrentDirectory();

            await dbContext.Database.EnsureCreatedAsync();
            if (!dbContext.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Regular" };
                foreach (string role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
                var path = File.ReadAllText(FilePath(baseDir, "Json/User.json"));

                var users = JsonConvert.DeserializeObject<List<User>>(path);

                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, "Password@123");
                    if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "Regular");
                    }
                }
            }
        }
        static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }


    }
}
