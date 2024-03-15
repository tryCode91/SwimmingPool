using Microsoft.AspNetCore.Identity;
using SwimmingPool_V1.Data;
using SwimmingPool_V1.Models;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using static System.Net.WebRequestMethods;

namespace SwimmingPool.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Pool.Any())
                {
                    context.Pool.AddRange(new List<Pool>()
                    {
                        new Pool()
                        {
                            PoolName = "Arena Älvsborg",
                            Location = "Trollhättan",
                            Open = new TimeOnly(9, 0, 0),
                            Close = new TimeOnly(21, 0, 0)
                        },
                        new Pool()
                        {
                            PoolName = "Badpalatset",
                            Location = "Göteborg",
                            Open = new TimeOnly(8, 0, 0),
                            Close = new TimeOnly(20, 0, 0)
                        },
                        new Pool()
                        {
                            PoolName = "Äventyrsbadet",
                            Location = "Vänersborg",
                            Open = new TimeOnly(10, 0, 0),
                            Close = new TimeOnly(22, 0, 0)
                        },

                    });
                    context.SaveChanges();
                }

                if (!context.Lane.Any())
                {

                    context.Lane.AddRange(new List<Lane>()
                    {
                        //Trollhättan
                        new Lane()
                        {
                            Limit = 5,
                            Type = "Crawl",
                            PoolId = 1,
                            Image = "https://source.unsplash.com/ZbbhkQ0M2AM",
                            
                        },
                        new Lane()
                        {
                            Limit = 5,
                            Type = "Breast stroke",
                            PoolId = 1,
                            Image = "https://source.unsplash.com/n2v3lTWy74Y",
                            
                        },
                        new Lane()
                        {
                            Limit = 5,
                            Type = "Kids area",
                            PoolId = 1,
                            Image = "https://source.unsplash.com/55MySYrKf5w",
                        },
                        new Lane()
                        {
                            Limit = 5,
                            Type = "Police training",
                            PoolId = 1,
                            Image = "https://source.unsplash.com/QGdRrty4054",
                        },

                        // Göteborg
                        new Lane()
                        {
                            Limit = 15,
                            Type = "Crawl",
                            PoolId = 2,
                            Image = "https://source.unsplash.com/ZbbhkQ0M2AM",
                        },
                        new Lane()
                        {
                            Limit = 20,
                            Type = "Breast stroke",
                            PoolId = 2,
                            Image = "https://source.unsplash.com/n2v3lTWy74Y",
                        },
                        new Lane()
                        {
                            Limit = 30,
                            Type = "Kids area",
                            PoolId = 2,
                            Image = "https://source.unsplash.com/55MySYrKf5w",
                        },
                        
                        // vänersborg
                        new Lane()
                        {
                            Limit = 2,
                            Type = "Crawl",
                            PoolId = 3,
                            Image = "https://source.unsplash.com/ZbbhkQ0M2AM",
                        },
                        new Lane()
                        {
                            Limit = 10,
                            Type = "Breast stroke",
                            PoolId = 3,
                            Image = "https://source.unsplash.com/n2v3lTWy74Y",
                        },
                        

                    });
                    context.SaveChanges();
                }

            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "petrusdughemdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "petrusdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Age = 32,

                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Age = 30,
                       
                       
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }


    }
}
