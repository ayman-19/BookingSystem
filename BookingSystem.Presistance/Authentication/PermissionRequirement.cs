using Microsoft.AspNetCore.Authorization;

namespace BookingSystem.Presistance.Authentication
{
    public class PermissionRequirement
        : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
