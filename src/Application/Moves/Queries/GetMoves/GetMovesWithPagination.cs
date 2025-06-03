using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Mappings;
using PokemonInHomeAPI.Application.Common.Models;
using PokemonInHomeAPI.Application.Common.Security;

namespace PokemonInHomeAPI.Application.Moves.Queries.GetMoves;

[Authorize]
public record GetMovesWithPaginationQuery : IRequest<PaginatedList<MoveDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPokemonsWithPaginationQueryHandler : IRequestHandler<GetMovesWithPaginationQuery, PaginatedList<MoveDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPokemonsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MoveDto>> Handle(GetMovesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Moves
            .OrderBy(x => x.Name)
            .ProjectTo<MoveDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
