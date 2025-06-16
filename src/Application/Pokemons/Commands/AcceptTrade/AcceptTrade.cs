using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Security;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.Events;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.AcceptTrade;

[Authorize(Roles = Roles.Player)]
public record AcceptTradeCommand : IRequest<string>
{
    public int TradeId { get; init; }
    public int RequestedPokemonId { get; init; }
}

public class AcceptTradeHandler : IRequestHandler<AcceptTradeCommand, string>
{
    private readonly IApplicationDbContext _context;

    public AcceptTradeHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(AcceptTradeCommand request, CancellationToken cancellationToken)
    {
        var trade = await _context.Trades
            .Include(t => t.TradePokemons)
            .FirstOrDefaultAsync(t => t.Id == request.TradeId, cancellationToken);

        if (trade?.TradeStatus != TradeStatus.Offered ||
            trade.Player2Id != request.RequestedPokemonId ||
            trade.TradePokemons.Count != 2)
        {
            throw new InvalidOperationException("Invalid trade acceptance");
        }
        
        // Get affected Pokemon IDs before swapping
        var affectedPokemonIds = trade.TradePokemons.Select(tp => tp.PokemonId).ToList();
        
        // Swap pokemon ownership
        foreach (var tp in trade.TradePokemons)
        {
            var pokemon = await _context.PlayerPokemons.FindAsync(tp.PokemonId);
            pokemon!.PlayerId = tp.PlayerId == trade.Player1Id ? trade.Player2Id : trade.Player1Id;
        }
        
        trade.TradeStatus = TradeStatus.Completed;

        // Raise domain events when trade pokemon are accepted (for rejected the same request from other player)
        trade.AddDomainEvent(new TradeAcceptedEvent(trade.Id, affectedPokemonIds));
        
        await _context.SaveChangesWithEventsAsync(cancellationToken);
        return trade.TradeStatus.ToString();
    }
}
