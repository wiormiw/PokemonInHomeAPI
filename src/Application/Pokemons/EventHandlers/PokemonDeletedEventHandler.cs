using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Events;

namespace PokemonInHomeAPI.Application.Pokemons.EventHandlers;

public class PokemonDeletedEventHandler : INotificationHandler<PokemonDeletedEvent>
{
    private readonly ILogger<PokemonDeletedEventHandler> _logger;
    private readonly IApplicationDbContext _context;

    public PokemonDeletedEventHandler(ILogger<PokemonDeletedEventHandler> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(PokemonDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        var relatedEntries = await _context.Pokedexes
            .Where(p => p.SpeciesId == notification.Pokemon.Id) // Pokemon.Id refer to PokemonSpecies that deleted
            .ToListAsync(cancellationToken);

        if (relatedEntries.Any())
        {
            _context.Pokedexes.RemoveRange(relatedEntries);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
