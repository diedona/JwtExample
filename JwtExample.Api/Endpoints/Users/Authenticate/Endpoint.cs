using JwtExample.Api.Endpoints.Base;
using JwtExample.Domain.Commands.Users.Authenticate;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JwtExample.Api.Endpoints.Users.Authenticate;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost("users/authenticate", HandleAsync);
    }

    public async ValueTask<Ok<AuthenticateResponse>> HandleAsync(
        [FromServices] ISender sender,
        [FromBody] AuthenticateCommand command
    )
    {
        var response = await sender.Send(command);
        return TypedResults.Ok(response);
    }
}
