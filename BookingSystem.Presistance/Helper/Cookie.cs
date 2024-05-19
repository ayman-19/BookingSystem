using Microsoft.AspNetCore.Http;

namespace BookingSystem.Presistance.Helper
{
    public class Cookie
    {
        private readonly IHttpContextAccessor _httpContext;
        public Cookie(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public void SetRefreshTokenInCookie(string refreshToken, DateTime expire)
        {
            var cookie = new CookieOptions
            {
                HttpOnly = true,
                Expires = expire.ToLocalTime(),
                Secure = true
            };
            _httpContext.HttpContext!.Response.Cookies.Append("RefreshToken", refreshToken, cookie);
        }
    }
}
