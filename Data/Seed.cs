using CarShops.Data.Enum;
using CarShops.Models;
using Microsoft.AspNetCore.Identity;

namespace CarShops.Data
{
    public class Seed
    {
        public static void SeedDate(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBContext>();

                context.Database.EnsureCreated();

                if (!context.Cars.Any())
                {
                    context.Cars.AddRange(new List<Car>()
                    {
                        new Car()
                        {
                            Name = "Tesla Model 3 Performance",
                            ShortDesc = "Сжигайте резину, а не бензин",
                            LongDesc = "Tesla Model 3 - это полностью электрический четырехдверный седан, производимый и продаваемый Tesla, Inc. На сегодняшний день это самый доступный автомобиль в модельном ряду Tesla.",
                            Image = "https://i.gaw.to/content/photos/36/50/365047_Tesla_Model_3.jpg?1024x640",
                            Price = 2500000,
                            Available = true,
                            categoryID = CarCategory.Electromobile
                         },

                        new Car()
                        {
                            Name = "BMW X5 M Competition",
                            ShortDesc = "С удовольствием за рулем!",
                            LongDesc = "Удивительная внушительность и несравненные динамические характеристики — вот благодаря чему BMW X5 M Competition поистине неподражаем.",
                            Image = "https://media.evo.co.uk/image/private/s--Jo-607-h--/v1618327267/evo/2021/04/Best%20performance%20SUVs%202021-3.jpg",
                            Price = 28442300,
                            Available = true,
                            categoryID = CarCategory.Gasoline
                         },

                        new Car()
                        {
                            Name = "Mercedes-Benz S-Класс AMG 63 e AMG Long",
                            ShortDesc = "Самое лучшее, или ничего",
                            LongDesc = "Главное в седане S-Класса – уникальный комфорт и технологии безопасности, на которые вы всегда можете рассчитывать и как водитель, и как пассажир. Будь то роскошные материалы отделки или новаторская концепция управления мультимедийной системой MBUX – каждая составляющая автомобиля создана, чтобы гарантировать незабываемые ощущения от поездки.",
                            Image = "http://auto.tcell.tj/uploads/news/image/207702/fixed1480x800_dd45207fc7cf7f57f152138cd3d20904.jpg",
                            Price = 36500000 ,
                            Available = true,
                            categoryID = CarCategory.Racing
                        }
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
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true
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
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
