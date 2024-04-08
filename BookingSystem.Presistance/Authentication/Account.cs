using BookingSystem.Application.IAuthentication;
using BookingSystem.Application.IRepositories;
using BookingSystem.Domain.Model;
using BookingSystem.DTOs.Authentication;
using BookingSystem.Presistance.Helper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookingSystem.Presistance.Authentication
{
    public class Account : IAccount
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataProtector _dataProtector;
        private readonly IConfiguration _cofig;
        private readonly jWTSettings _jWt;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;

        public Account(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager, IOptionsMonitor<jWTSettings> jWt, IEmailService emailService, IDataProtectionProvider provider, IConfiguration cofig)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _jWt = jWt.CurrentValue;
            _emailService = emailService;
            _cofig = cofig;
            _dataProtector = provider.CreateProtector(_cofig["dataProtection:key"]);
        }

        public async Task<AuthenticateResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            User user = new()
            {
                BirthDate = registerRequest.BirthDate,
                PhoneNumber = registerRequest.Phone,
                Address = registerRequest.Address,
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                UserName = registerRequest.UsarName
            };
            var addUser = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!addUser.Succeeded)
            {
                var strBuilder = new StringBuilder();
                foreach (var error in addUser.Errors)
                    strBuilder.Append(error.Description);
                return new AuthenticateResponse { Massage = strBuilder.ToString() };
            }
            var addrole = await _userManager.AddToRoleAsync(user, "User");
            if (!addrole.Succeeded)
            {
                var strBuilder = new StringBuilder();
                foreach (var error in addrole.Errors)
                    strBuilder.Append(error.Description);
                return new AuthenticateResponse { Massage = strBuilder.ToString() };
            }
            var token = await CreateAccessTokenAsync(user);
            var refToken = CreateRefreshToken();
            refToken.AccessToken = token;
            refToken.AccessTokenExpiration = DateTime.Now.AddDays(_jWt.AccessTokenExiretionDate);
            user.RefreshTokens.Add(refToken);
            await _userManager.UpdateAsync(user);
            var tokenConfirmEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailService.SendMailMessageAsync(user, tokenConfirmEmail);
            await _unitOfWork.CommitAsync();
            return new AuthenticateResponse
            {
                AccessToken = token,
                AccessTokenExpireOn = refToken.AccessTokenExpiration,
                IsAuthanticated = true,
                RefreshToken = refToken.RefreshJwtToken,
                RefreshTokenExpireOn = refToken.ExpireOn,
                UserName = user.Name,
                UserId = user.Id,
            };
        }
        public async Task<AuthenticateResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);
            await _unitOfWork.CommitAsync();
            if (!result.Succeeded)
                return new AuthenticateResponse { Massage = result.ToString() };
            if (!user.EmailConfirmed)
                return new AuthenticateResponse { Massage = "Must be Confirm Email!" };
            if (!user.RefreshTokens.Any(re => re.IsValid))
            {
                var accessToken = await CreateAccessTokenAsync(user);
                var refToken = CreateRefreshToken();
                refToken.AccessToken = accessToken;
                refToken.AccessTokenExpiration = DateTime.Now.AddDays(_jWt.AccessTokenExiretionDate);
                user.RefreshTokens.Add(refToken);
                await _userManager.UpdateAsync(user);
                await _unitOfWork.CommitAsync();
            }
            var refTokenForUser = user.RefreshTokens.First(re => re.IsValid);
            return new AuthenticateResponse
            {
                AccessToken = refTokenForUser.AccessToken,
                AccessTokenExpireOn = refTokenForUser.AccessTokenExpiration,
                IsAuthanticated = true,
                RefreshToken = refTokenForUser.RefreshJwtToken,
                RefreshTokenExpireOn = refTokenForUser.ExpireOn,
                UserId = user.Id,
                UserName = user.Name
            };
        }
        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var confirmed = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmed.Succeeded)
            {
                var strBuilder = new StringBuilder();
                foreach (var error in confirmed.Errors)
                    strBuilder.Append(error.Description);
                return strBuilder.ToString();
            }
            await _unitOfWork.CommitAsync();
            return "Email is Confirmed";
        }
        public async Task<AuthenticateResponse> RefreshTokenAsync(string token)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.RefreshTokens.Any(re => re.RefreshJwtToken == token), astracking: true);
            var deleteRefToken = user.RefreshTokens.First(re => re.RefreshJwtToken == token);
            user.RefreshTokens.Remove(deleteRefToken);

            var accessToken = await CreateAccessTokenAsync(user);
            var refToken = CreateRefreshToken();
            refToken.AccessToken = accessToken;
            refToken.AccessTokenExpiration = DateTime.Now.AddDays(_jWt.AccessTokenExiretionDate);
            user.RefreshTokens.Add(refToken);
            await _unitOfWork.SaveChanges();
            await _unitOfWork.CommitAsync();
            return new AuthenticateResponse
            {
                AccessToken = accessToken,
                AccessTokenExpireOn = refToken.AccessTokenExpiration,
                IsAuthanticated = true,
                RefreshToken = refToken.RefreshJwtToken,
                RefreshTokenExpireOn = refToken.ExpireOn,
                UserName = user.Name,
                UserId = user.Id,
            };
        }
        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.RefreshTokens.Any(re => re.RefreshJwtToken == token), astracking: true);
            var revokeRefToken = user.RefreshTokens.First(re => re.RefreshJwtToken == token);
            revokeRefToken.RevokeOn = DateTime.UtcNow;
            await _unitOfWork.SaveChanges();
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<string> SendResetPasswordCodeAsync(string email)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Email == email, astracking: true);
            var random = new Random();
            var code = random.Next(1, 100000).ToString("D6");
            user.Code = _dataProtector.Protect(code);
            await _unitOfWork.SaveChanges();
            await _unitOfWork.CommitAsync();
            return await _emailService.SendEmailAsync(email, $"Code To Reset Password {code}", user.Name);
        }
        public async Task<string> ResetPasswordAsync(string email, string newPassword, string code)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Email == email, astracking: true);
            var codeUser = _dataProtector.Unprotect(user.Code);
            if (codeUser != code)
                return "Code InCorrect";
            var removePassword = await _userManager.RemovePasswordAsync(user);
            if (!removePassword.Succeeded)
                return string.Join(", ", removePassword.Errors.Select(e => e.Description));
            var addPassword = await _userManager.AddPasswordAsync(user, newPassword);
            if (!addPassword.Succeeded)
                return string.Join(", ", addPassword.Errors.Select(e => e.Description));
            await _unitOfWork.CommitAsync();
            return "Ok";
        }
        private async Task<string> CreateAccessTokenAsync(User user)
        {
            var userClaim = await _userManager.GetClaimsAsync(user);
            var roleForUser = await _userManager.GetRolesAsync(user);

            var roleClaime = new List<Claim>();
            foreach (var role in roleForUser)
                roleClaime.Add(new Claim(ClaimTypes.Role, role));
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.PrimarySid, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(CustomClaims.Permission, "Per"),
                new Claim(CustomClaims.ExpireAccessTokenOn, $"{DateTime.Now.AddDays(_jWt.AccessTokenExiretionDate)}")
            };
            roleClaime.AddRange(userClaims);
            roleClaime.AddRange(userClaim);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityTokentDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jWt.Audience,
                Issuer = _jWt.Issuer,
                //Expires = DateTime.Now.AddMinutes(_jWt.AccessTokenExiretionDate),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWt.Key)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(roleClaime)
            };
            var securityToken = tokenHandler.CreateToken(securityTokentDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
        private RefreshToken CreateRefreshToken()
        {
            var token = new byte[64];
            var generator = RandomNumberGenerator.Create();
            generator.GetBytes(token);
            return new RefreshToken
            {
                RefreshJwtToken = Convert.ToBase64String(token),
                CreateOn = DateTime.Now,
                ExpireOn = DateTime.Now.AddMonths((int)_jWt.RefreshTokenExiretionDate)
            };
        }
        public async Task DeleteUserAsync(string userId)
        {
            await _unitOfWork.Users.DeleteAsync(await _unitOfWork.Users.GetAsync(u => u.Id == userId, astracking: true));
            await _unitOfWork.SaveChanges();
            await _unitOfWork.CommitAsync();
        }
    }
}
