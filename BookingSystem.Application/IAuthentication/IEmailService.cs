using BookingSystem.Domain.Model;

namespace BookingSystem.Application.IAuthentication
{
	public interface IEmailService
	{
		Task SendMailMessageAsync(User user, string token);
		Task<string> SendEmailAsync(string email, string message, string userName);
	}
}
