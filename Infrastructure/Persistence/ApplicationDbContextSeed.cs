using Application.Common.Models;
using Domain.Entities;

using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var administratorRole = new IdentityRole("Administrator");

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
            roleManager.AddClaimAsync(administratorRole, new System.Security.Claims.Claim("Permission", Admin.admin));
        }

        var administrator = new ApplicationUser { UserName = "admin", Email = "admin" };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Admin123@");
            await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }
    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        // Seed, if necessary
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(new Category[]
            {
                new Category { name = "Berlin", code="1"},
                new Category { name = "Hamburg", code="2" },
                new Category { name = "München", code="3" },
                new Category { name = "Köln", code="4" },
                new Category { name = "Frankfurt am Main", code="5" },
                new Category { name = "Stuttgart" , code = "6"},
                new Category { name = "Düsseldorf" , code="7"}
            });

            await context.SaveChangesAsync();
        }
    }
}
