using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core
{
  public  class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>  where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var failures = new List<FluentValidation.Results.ValidationFailure>();

                foreach (var validator in _validators)
                {
                    var result = await validator.ValidateAsync(request, cancellationToken);
                    if (!result.IsValid)
                        failures.AddRange(result.Errors);
                }

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            return await next(); // Proceed to the Handler if everything is okay
        }
    }
   
}
