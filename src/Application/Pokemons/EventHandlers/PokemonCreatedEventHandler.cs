using Microsoft.Extensions.Logging;
using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.Events;

namespace PokemonInHomeAPI.Application.Pokemons.EventHandlers;

public class PokemonCreatedEventHandler : INotificationHandler<PokemonCreatedEvent>
{
    private readonly ILogger<PokemonCreatedEventHandler> _logger;
    private readonly IApplicationDbContext _context;

    public PokemonCreatedEventHandler(ILogger<PokemonCreatedEventHandler> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(PokemonCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("PokemonInHomeAPI Domain Event: {DomainEvent}", notification.GetType().Name);

        var allPlayerIds = await _context.Players
            .Select(p => p.Id)
            .ToListAsync(cancellationToken);

        foreach (var playerId in allPlayerIds)
        {
            var pokedexEntryExists = await _context.Pokedexes
                .AnyAsync(p => p.PlayerId == playerId && p.SpeciesId == notification.Pokemon.Id, cancellationToken); // Pokemon.Id refer to PokemonSpecies that created.

            if (!pokedexEntryExists)
            {
                _context.Pokedexes.Add(new Pokedex
                {
                    PlayerId = playerId,
                    SpeciesId = notification.Pokemon.Id,
                    Seen = false,
                    Caught = false
                });
            }
        }
    }
}
