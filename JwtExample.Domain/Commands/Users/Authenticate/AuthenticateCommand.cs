using MediatR;

namespace JwtExample.Domain.Commands.Users.Authenticate;

public record AuthenticateCommand(string Username, string Password) : IRequest<AuthenticateResponse>;