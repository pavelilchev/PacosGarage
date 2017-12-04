namespace Autoshop.Web.Extensions
{
    using Autoshop.Data;
    using Autoshop.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    using static Autoshop.Common.Constants.CommonConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigrate(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AutoshopDbContext>().Database.Migrate();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    string[] roleNames = { Admin, Writer };

                    foreach (var roleName in roleNames)
                    {
                        var roleExist = await roleManager.RoleExistsAsync(roleName);
                        if (!roleExist)
                        {
                            await roleManager.CreateAsync(new IdentityRole(roleName));
                        }
                    }

                    var adminMail = configuration["AdminCredentials:Email"];
                    var admin = await userManager.FindByEmailAsync(adminMail);

                    if (admin == null)
                    {
                        admin = new User
                        {
                            UserName = adminMail,
                            Email = adminMail,
                            FirstName = Admin,
                            LastName = Admin
                        };

                        string userPWD = configuration["AdminCredentials:Password"];
                        var createAdmin = await userManager.CreateAsync(admin, userPWD);
                        if (createAdmin.Succeeded)
                        {
                            await userManager.AddToRoleAsync(admin, Admin);
                        }
                    }
                }).GetAwaiter().GetResult();
            }

            return app;
        }
    }
}
