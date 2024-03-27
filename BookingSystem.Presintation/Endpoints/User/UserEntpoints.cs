using BookingSystem.Application.Feature.User.Commands.Request;
using BookingSystem.Application.Feature.User.Queries.Request;
using BookingSystem.DTOs.Authentication;
using BookingSystem.Presistance.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Presintation.Endpoints.User
{
	public static class UserEntpoints
	{
		public static void MapUserEntpoints(this IEndpointRouteBuilder builder)
		{
			var map = builder.MapGroup("Users");
			map.MapPost("/RegisterAysnc", async (DTOs.Authentication.RegisterRequest register, ISender _sender, Cookie cookie) =>
			{
				var response = await _sender.Send(new Application.Feature.User.Commands.Request.RegisterRequest(register));
				cookie.SetRefreshTokenInCookie(response.RefreshToken, response.RefreshTokenExpireOn);
				return response;
			});
			map.MapPost("/LoginAysnc", async (LoginRequest login, ISender _sender, Cookie cookie) =>
			{
				var result = await _sender.Send(new LoginUserRequest(login));
				cookie.SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpireOn);
				return result;
			});
			map.MapGet("/RefreshTokenAysnc", async (ISender _sender, IHttpContextAccessor _httpContext, Cookie cookie) =>
			{
				var result = await _sender.Send(new RefreshTokenRequest(_httpContext.HttpContext!.Request.Cookies["RefreshToken"]!));
				cookie.SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpireOn);
				return result;
			});
			map.MapGet("/ConfirmEmailAsync", async ([FromQuery] string token, [FromQuery] string userId, ISender _sender) =>
			{
				return await _sender.Send(new ConfirmEmailRequest(userId, token));
			});
			map.MapGet("/RevokeTokenAysnc", async (ISender _sender, IHttpContextAccessor _httpContext) =>
			{
				await _sender.Send(new RevokeTokenRequest(_httpContext.HttpContext!.Request.Cookies["RefreshToken"]!));
			});
			map.MapGet("/SendResetPasswordCodeAsync", async (string email, ISender _sender) =>
			{
				return await _sender.Send(new SendResetPasswordCodeRequest(email));
			});
			map.MapGet("/ResetPasswordAysnc", async (string newPassword, string code, ISender _sender) =>
			{
				return await _sender.Send(new ResetPasswordRequest(newPassword, code));
			});
			map.MapDelete("/DeleteUserAysnc", async (string userId, ISender _sender) =>
			{
				await _sender.Send(new DeleteUserRequest(userId));
			});
		}
	}
}
