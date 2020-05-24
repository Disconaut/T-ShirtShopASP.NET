using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Online_T_Shirt_Shop.Areas.Identity.Data;
using Online_T_Shirt_Shop.Areas.Identity.Data.Enums;

namespace Online_T_Shirt_Shop.Data.Seeder
{
    public class IdentityInitializer
    {
        public static void SeedData
        (UserManager<Consumer> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager, config);
        }

        public static void SeedUsers
            (UserManager<Consumer> userManager, IConfiguration config)
        {
            if (userManager.FindByNameAsync
                    (config["AdminUsername"]).Result != null) return;
            var user = new Consumer
            {
                UserName = config["AdminUsername"], Email = config["AdminEmail"],
                EmailConfirmed = true
            };

            var result = userManager.CreateAsync
                (user, config["AdminPassword"]).Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user,
                    Roles.Admin.ToString()).Wait();
            }
        }
        
        public static void SeedRoles
            (RoleManager<IdentityRole> roleManager)
        {
            foreach (var name in Enum.GetNames(typeof(Roles)))
            {
                if (roleManager.RoleExistsAsync
                    (name).Result) continue;
                var role = new IdentityRole
                {
                    Name = name
                };
                var roleResult = roleManager.
                    CreateAsync(role).Result;
            }
        }
    }
}
