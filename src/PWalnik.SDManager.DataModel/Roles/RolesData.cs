using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PWalnik.SDManager.DataModel.Roles
{
    public static class RolesData
    {
        private static readonly string[] Roles = new[] {
            "Administrator",
            "Manager",
            "SalesRepresentative"
        };

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var create = await roleManager.CreateAsync(new IdentityRole(role));

                    if (!create.Succeeded)
                    {
                        throw new Exception("Failed to create role");
                    }
                }
            }
        }
    }
}
