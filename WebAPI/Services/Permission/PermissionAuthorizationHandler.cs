using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Services.Permission;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User == null)
        {
            return;
        }
        var permissionss = context.User.Claims.Where(x => x.Type == "Permission" &&
                                                       requirement.Permission.Contains(x.Value));
        if (permissionss.Any())
        {
            context.Succeed(requirement);
            return;
        }
        else if (requirement.Permission == UserDefault.AllUserAccess)
        {
            context.Succeed(requirement);
            return;
        }
    }
}
