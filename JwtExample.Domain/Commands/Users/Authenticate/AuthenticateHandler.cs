using MediatR;

namespace JwtExample.Domain.Commands.Users.Authenticate;

public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
{
    public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        return new AuthenticateResponse($"Hello there, {request.Username}!");
    }
}
