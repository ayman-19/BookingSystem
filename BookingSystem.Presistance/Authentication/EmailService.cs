using BookingSystem.Application.IAuthentication;
using BookingSystem.Domain.Model;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BookingSystem.Presistance.Authentication
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        //private readonly IUrlHelper _urlHelper;
        public EmailService(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _httpContext = httpContext;
        }
        public async Task SendMailMessageAsync(User user, string token)
        {

            var request = _httpContext.HttpContext.Request;
            var querystring = request.QueryString.Add(name: "token", token);
            var url =
                $"{request.Scheme}://{request.Host}/Users/ConfirmEmailAsync{querystring}&userId={user.Id}";
            //{ _urlHelper.Action("ConfirmEmailAsync", "Users", new { userId = user.Id, token = token })}
            var message = $"To Confirm Email Click Link: <a href='{url}'>Confirm</a>";
            await SendEmailAsync(user.Email!, message, user.Name);
        }
        public async Task<string> SendEmailAsync(string email, string message, string userName)
        {
            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]!), true);
                await smtp.AuthenticateAsync(_configuration["Email:gmail"], _configuration["Email:password"]);
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $"{message}",
                    TextBody = "Wellcome",
                };
                var sendMessage = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody(),
                    Subject = $"Wellcome {userName}"
                };
                sendMessage.From.Add(new MailboxAddress("Ayman Roshdy", _configuration["Email:gmail"]));
                sendMessage.To.Add(new MailboxAddress(userName, email));
                await smtp.SendAsync(sendMessage);
                smtp.Disconnect(true);
            }
            return "Success";
        }
    }
}
