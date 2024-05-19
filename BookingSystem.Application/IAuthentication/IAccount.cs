using BookingSystem.DTOs.Authentication;

namespace BookingSystem.Application.IAuthentication
{
    public interface IAccount
    {
        Task<AuthenticateResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<AuthenticateResponse> LoginAsync(LoginRequest loginRequest);
        Task<AuthenticateResponse> RefreshTokenAsync(string token);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<string> SendResetPasswordCodeAsync(string email);
        Task<string> ResetPasswordAsync(string email, string newPassword, string code);
        Task<int> DeleteUserAsync(string userId);
        Task<string> AddRoleAsync(string role);
        Task<string> RemoveRoleAsync(string role);
        Task<string> AddRoleToUserAsync(string role, string userId);
        Task AddPermissionAsync(string permission);
    }
}
