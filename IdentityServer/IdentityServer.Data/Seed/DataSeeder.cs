using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;

namespace IdentityServer.Data.Seed
{
    public class DataSeeder
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DataSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Seed()
        {
            var roleNames = new[] { "Admin", "User" };
            foreach (var item in roleNames)
            {
                if (!roleManager.RoleExistsAsync(item).Result)
                {
                    roleManager.CreateAsync(new IdentityRole(item)).Wait();
                }
            }

            var email = "admin@admin.com";
            if (userManager.FindByEmailAsync(email).Result is null)
            {
                var user = new IdentityUser()
                {
                    Email = email,
                    UserName = email,
                    PhoneNumber = "09304241296",
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,

                };
                userManager.CreateAsync(user, "Pa$$word").Wait();
                userManager.AddToRoleAsync(user, roleNames[0]).Wait();
                userManager.AddClaimAsync(user, new Claim("Permission", "v1/User/AddClaim")).Wait();
            }
        }
    }
}
