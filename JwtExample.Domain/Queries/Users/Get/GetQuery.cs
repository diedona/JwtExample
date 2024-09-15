using MediatR;

namespace JwtExample.Domain.Queries.Users.Get;

public record GetQuery() : IRequest<GetResponse>;