using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;

namespace Notes.Application.Common.Behaviors
{
    /// <summary>
    /// чтобы валидация работала, нужно ее встроить в pipeline медиатора
    /// TRequest - объект запроса, переданный через метод Mediatr.Send()
    /// next - асинхронное продолжение для следующего действия в цепочке вызовов нашего Behavior
    /// т.к. next не принимает TRequest в качестве параметра, то мы можем изменять входной запрос, но не заменять его
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehavior<TRequest,TResponse>
        :IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) =>
            _validators = validators;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken
            ,RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();
            if(failures.Count!=0)
            {
                throw new ValidationException(failures);
            }
            return next();
        }
    }
}
