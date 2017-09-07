using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PWalnik.SDManager.DataModel.Data;

namespace PWalnik.SDManager.DataModel.Models
{
    public static class AdminData
    {
        public static async Task SeedAdminAccountAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var role = await roleManager.FindByNameAsync("Administrator");
                var userRole = new IdentityUserRole<string> { RoleId = role.Id };

                var user = new ApplicationUser {UserName = "pawel.walnik@admin.com", Email = "pawel.walnik@admin.com"};
                user.Roles.Add(userRole);

                string password = "Admin1.";
                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create admin account");
                }             
            }
        }
    }
}
