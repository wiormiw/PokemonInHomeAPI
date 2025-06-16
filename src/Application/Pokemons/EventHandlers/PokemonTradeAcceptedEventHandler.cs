using Microsoft.Extensions.Logging;
using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.Events;

namespace PokemonInHomeAPI.Application.Pokemons.EventHandlers;

public class PokemonTradeAcceptedEventHandler : INotificationHandler<TradeAcceptedEvent>
{
    private readonly ILogger<PokemonTradeAcceptedEventHandler> _logger;
    private readonly IApplicationDbContext _context;

    public PokemonTradeAcceptedEventHandler(
        ILogger<PokemonTradeAcceptedEventHandler> logger, 
        IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(TradeAcceptedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing trade acceptance for trade {TradeId}", notification.TradeId);

        // Find all pending trades involving these Pokémon
        var conflictingTrades = await _context.Trades
            .Include(t => t.TradePokemons)
            .Where(t => t.TradeStatus == TradeStatus.Offered &&
                        t.Id != notification.TradeId &&
                        t.TradePokemons.Any(tp => notification.AffectedPokemonIds.Contains(tp.PokemonId)))
            .ToListAsync(cancellationToken);

        foreach (var trade in conflictingTrades)
        {
            trade.TradeStatus = TradeStatus.Rejected;
            _logger.LogInformation("Auto-rejected trade {ConflictTradeId} due to completed trade {CompletedTradeId}", 
                trade.Id, notification.TradeId);
        }
    }
}
