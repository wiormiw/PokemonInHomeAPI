using Microsoft.Extensions.Logging;
using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.Events;

namespace PokemonInHomeAPI.Application.Pokemons.EventHandlers;

public class PokemonCaughtEventHandler : INotificationHandler<PokemonCaughtEvent>
{
    private readonly ILogger<PokemonCaughtEventHandler> _logger;
    private readonly IApplicationDbContext _context;

    public PokemonCaughtEventHandler(ILogger<PokemonCaughtEventHandler> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(PokemonCaughtEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PokemonInHomeAPI Domain Event: {DomainEvent}", notification.GetType().Name);

        // Update pokedex when user caught a pokemon
        var pokedex = await _context.Pokedexes
            .FirstOrDefaultAsync(p => p.PlayerId == notification.PlayerId && p.SpeciesId == notification.SpeciesId);

        if (pokedex is not null)
        {
            pokedex.Seen = true;
            pokedex.Caught = true;
        }
        else
        {
            _context.Pokedexes.Add(new Pokedex
            {
                PlayerId = notification.PlayerId,
                SpeciesId = notification.SpeciesId,
                Seen = true,
                Caught = true
            });
        }
    }
}
