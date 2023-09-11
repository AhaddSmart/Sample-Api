using Application.Common.Models;
using Domain.Entities;
using Domain.Entities.Sample;
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
            context.Categories.AddRange(new BusinessCategory[]
            {
                new BusinessCategory { name = "Berlin", code="1"},
                new BusinessCategory { name = "Hamburg", code="2" },
                new BusinessCategory { name = "München", code="3" },
                new BusinessCategory { name = "Köln", code="4" },
                new BusinessCategory { name = "Frankfurt am Main", code="5" },
                new BusinessCategory { name = "Stuttgart" , code = "6"},
                new BusinessCategory { name = "Düsseldorf" , code="7"}
            });

            await context.SaveChangesAsync();
        }
    }
}
