using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Presistance.Authentication
{
    public class PermissionHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var canAccess = await Task.FromResult(context.User.Claims
                .Any(t => t.Type == "Permission" && t.Value == requirement.Permission));
            if (canAccess)
                context.Succeed(requirement);
        }
    }
}
