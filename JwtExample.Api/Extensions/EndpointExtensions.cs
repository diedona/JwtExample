using JwtExample.Api.Endpoints.Base;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace JwtExample.Api.Extensions;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var endpointsToBeInjected = assembly
            .DefinedTypes
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false } &&
                type.IsAssignableTo(typeof(IEndpoint))
            ).Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type));

        services.TryAddEnumerable(endpointsToBeInjected);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
