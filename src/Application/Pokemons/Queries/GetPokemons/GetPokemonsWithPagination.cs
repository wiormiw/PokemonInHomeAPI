using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Mappings;
using PokemonInHomeAPI.Application.Common.Models;
using PokemonInHomeAPI.Application.Common.Security;

namespace PokemonInHomeAPI.Application.Pokemons.Queries.GetPokemons;

[Authorize]
public record GetPokemonsWithPaginationQuery : IRequest<PaginatedList<PokemonDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPokemonsWithPaginationQueryHandler : IRequestHandler<GetPokemonsWithPaginationQuery, PaginatedList<PokemonDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPokemonsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PokemonDto>> Handle(GetPokemonsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.PokemonSpecies
            .OrderBy(x => x.Name)
            .ProjectTo<PokemonDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
