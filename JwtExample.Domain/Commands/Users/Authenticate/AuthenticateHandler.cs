using JwtExample.CrossCutting.Options;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace JwtExample.Domain.Commands.Users.Authenticate;

public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
{
    private readonly JwtOptions _JwtOptions;

    public AuthenticateHandler(
        IOptions<JwtOptions> jwtOptions
    )
    {
        _JwtOptions = jwtOptions.Value;
    }

    public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_JwtOptions.Key);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
        );

        var token = GenerateToken(request, credentials, handler);
        return new AuthenticateResponse(token);
    }

    private string GenerateToken(
        AuthenticateCommand request, 
        SigningCredentials credentials,
        JwtSecurityTokenHandler handler
    )
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(request),
            Expires = DateTime.UtcNow.AddMinutes(_JwtOptions.MinutesToLive),
            SigningCredentials = credentials,
            Issuer = _JwtOptions.Authority,
            Audience = _JwtOptions.Audience
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(AuthenticateCommand request)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, request.Username));

        return claims;
    }
}
