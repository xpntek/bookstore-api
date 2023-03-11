using Application.Behaviors;
using Application.Features.Books;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequisitionValidationPipelineBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionPipelineBehavior<,>));
        
        services.AddControllers()
            .AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<ListBooksAuthors.ListBooksAuthorsQuery>();
                s.DisableDataAnnotationsValidation = true;
            });
        
        services.AddMediatR(typeof(ListBooksAuthors.ListBooksAuthorsQuery).Assembly);
        
        return services;
    }
}