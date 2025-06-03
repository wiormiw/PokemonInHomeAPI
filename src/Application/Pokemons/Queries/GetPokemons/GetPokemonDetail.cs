using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Application.Pokemons.Queries.GetPokemons;

using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Mappings;
using PokemonInHomeAPI.Application.Common.Models;
using PokemonInHomeAPI.Application.Common.Security;

[Authorize]
public record GetPokemonQuery : IRequest<PokemonDto>
{
    public int PokemonId { get; init; }
}

public class GetPokemonQueryHandler : IRequestHandler<GetPokemonQuery, PokemonDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPokemonQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PokemonDto> Handle(GetPokemonQuery request, CancellationToken cancellationToken)
    {
        var pokemon = await _context.PokemonSpecies
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.PokemonId, cancellationToken);

        if (pokemon is null)
        {
            throw new NotFoundException(nameof(PokemonSpecies), $"{request.PokemonId}");
        }
        
        return _mapper.Map<PokemonDto>(pokemon);
    }
}

