using JwtExample.Api.Endpoints.Base;
using JwtExample.Domain.Queries.Users.Get;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JwtExample.Api.Endpoints.Users.List;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("users", HandleAsync)
            .RequireAuthorization();
    }

    public async ValueTask<Ok<GetResponse>> HandleAsync(
        [FromServices] ISender sender    
    )
    {
        var response = await sender.Send(new GetQuery());
        return TypedResults.Ok(response);
    }
}
