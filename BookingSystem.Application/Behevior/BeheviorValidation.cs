using FluentValidation;
using MediatR;

namespace BookingSystem.Application.Behevior
{
	public class BeheviorValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;
		public BeheviorValidation(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (_validators.Any())
			{
				var contextValidation = new ValidationContext<TRequest>(request);
				var validationResults = await Task.WhenAll(_validators.Select(val => val.ValidateAsync(contextValidation)));
				var filiar = validationResults.SelectMany(valerror => valerror.Errors);
				if (filiar.Count() > 0)
					throw new ValidationException(string.Join(", ", filiar.Select(er => er.ErrorMessage)));
			}
			return await next();
		}
	}
}
