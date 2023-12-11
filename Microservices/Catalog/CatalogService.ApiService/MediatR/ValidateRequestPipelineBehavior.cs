using FluentValidation.Results;

using SharedLib.Contracts;

namespace CatalogService.ApiService.MediatR;

public sealed class ValidateRequestPipelineBehavior<TRequest, TResponse>(
        IServiceScopeFactory serviceScopeFactory)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var validator =
            scope.ServiceProvider.GetService<IValidator<TRequest>>();

        if (validator != null)
        {
            var result = validator.Validate(request);
            switch (result.IsValid)
            {
                case false when IsRequestResults():
                    return (TResponse)CreateResponse(result.Errors);
                case false:
                    throw new ValidationException(result.Errors);
            }
        }

        return await next();
    }

    private static bool IsRequestResults()
    {
        if (typeof(TResponse).IsAbstract ||
            !typeof(TResponse).IsGenericType)
        {
            return false;
        }

        var genericType = typeof(TResponse).GetGenericTypeDefinition();
        return genericType == typeof(RequestResults<,>)
               || genericType == typeof(RequestResults<,,>)
               || genericType == typeof(RequestResults<,,,>);
    }

    private static object CreateResponse(IEnumerable<ValidationFailure> errors)
    {
        return RequestResults.ValidationProblem(errors);
    }
}