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
            context.Categories.AddRange(new Category[]
            {
                new Category { Name = "Berlin", Code="1"},
                new Category { Name = "Hamburg", Code="2" },
                new Category { Name = "München", Code="3" },
                new Category { Name = "Köln", Code="4" },
                new Category { Name = "Frankfurt am Main", Code="5" },
                new Category { Name = "Stuttgart" , Code = "6"},
                new Category { Name = "Düsseldorf" , Code="7"}
            });

            await context.SaveChangesAsync();
        }
    }
}
