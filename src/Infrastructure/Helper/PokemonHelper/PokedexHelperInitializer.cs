using Microsoft.EntityFrameworkCore;
using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Helper.PokemonHelper;

public static class PokemonHelperInitializer
{
    public static async Task AddEmptyPokedexForPlayerAsync(
        IApplicationDbContext context, 
        int playerId, 
        CancellationToken cancellationToken = default)
    {
        var speciesList = await context.PokemonSpecies
            .Select(s => s.Id)
            .ToListAsync(cancellationToken);

        var pokedexEntries = speciesList.Select(speciesId => new Pokedex
        {
            PlayerId = playerId,
            SpeciesId = speciesId,
            Seen = false,
            Caught = false
        }).ToList();

        await context.Pokedexes.AddRangeAsync(pokedexEntries, cancellationToken);
    }
}
