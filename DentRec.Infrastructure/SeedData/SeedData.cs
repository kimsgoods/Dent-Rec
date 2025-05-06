using DentRec.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DentRec.Infrastructure.SeedData
{
    public static class SeedData
    {

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {

            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!userManager.Users.Any(x => x.UserName == "admin@seclinic.com"))
            {
                var user = new AppUser
                {
                    FirstName = "Admin",
                    LastName = "Escabarte",
                    UserName = "admin@seclinic.com",
                    Email = "admin@seclinic.com",
                };

                await userManager.CreateAsync(user, "P@ssw0rd1234");
                await userManager.AddToRoleAsync(user, "Admin");
            }

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.Procedures.Any())
            {
                var proceduresData = await File.ReadAllTextAsync(path + @"/SeedData/procedures.json");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() }
                };

                var procedures = JsonSerializer.Deserialize<List<Procedure>>(proceduresData, options);

                if (procedures == null) return;

                context.Procedures.AddRange(procedures);

                await context.SaveChangesAsync();
            }
        }
    }
}
