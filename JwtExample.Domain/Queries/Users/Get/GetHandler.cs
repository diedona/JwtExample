using MediatR;

namespace JwtExample.Domain.Queries.Users.Get;

public class GetHandler : IRequestHandler<GetQuery, GetResponse>
{
    public async Task<GetResponse> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        return new GetResponse(["Diego", "José", "Hyonz", "John"]);
    }
}
