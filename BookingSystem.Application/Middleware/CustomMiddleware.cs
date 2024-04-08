using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace BookingSystem.Application.Middleware
{
    public class CustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    var tokenExpireDate = context.User?.Claims.First(u => u.Type == "ExpireAccessTokenOn")?.Value;
                    if (DateTime.Parse(tokenExpireDate).Date < DateTime.UtcNow.Date)
                        throw new Exception("Token Expire On Please Login Again");
                }
                await next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new { Message = ex.Message, IsSuccessfuly = false, StatusCode = GetStatusCode(ex) };
                var serialize = JsonSerializer.Serialize(responseModel);
                response.StatusCode = (int)GetStatusCode(ex);
                await response.WriteAsync(serialize);
            }
        }
        private HttpStatusCode GetStatusCode(Exception exception)
            => exception switch
            {
                ValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
    }
}
