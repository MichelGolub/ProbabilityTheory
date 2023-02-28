using Microsoft.AspNetCore.Identity;
using ProbabilityTheory.Identity.Models;

namespace ProbabilityTheory.Identity.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync
            (UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string username = "professor";
            string firstname = "Petro";
            string lastname = "Ivanov";
            string password = "professor1";
            if(await roleManager.FindByNameAsync("professor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("professor"));
            }
            if(await roleManager.FindByNameAsync("student") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("student"));
            }
            if(await userManager.FindByNameAsync(username) == null)
            {
                AppUser professor = new AppUser
                {
                    UserName = username,
                    FirstName = firstname,
                    LastName = lastname
                };
                IdentityResult result = await userManager.CreateAsync(professor, password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(professor, "professor");
                }
            }
        }
    }
}
