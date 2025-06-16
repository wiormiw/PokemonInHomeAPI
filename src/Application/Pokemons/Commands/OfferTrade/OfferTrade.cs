using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Security;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.OfferTrade;

[Authorize(Roles = Roles.Player)]
public record OfferTradeCommand : IRequest<int>
{
    public int Player2Id { get; init; }
    public int OfferedPokemonId { get; init; }
    public int RequestedPokemonId { get; init; }
}

public class OfferTradeCommandHandler : IRequestHandler<OfferTradeCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public OfferTradeCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(OfferTradeCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;
        
        if (userId is null) 
            throw new UnauthorizedAccessException(ErrorMessage.UnauthorizedErrorMessage);
        
        var player1 = await _context.Players
            .FirstOrDefaultAsync(p => p.ApplicationUserId == userId, cancellationToken);

        Guard.Against.NotFound(userId, player1);
        
        var player2 = await _context.Players
            .FirstOrDefaultAsync(p => p.Id == request.Player2Id, cancellationToken);
        
        Guard.Against.NotFound(request.Player2Id, player2);

        var tradePokemon1 = new TradePokemon
        {
            PlayerId = player1.Id,
            PokemonId = request.OfferedPokemonId,
        };

        var tradePokemon2 = new TradePokemon
        {
            PlayerId = player2.Id,
            PokemonId = request.RequestedPokemonId,
        };

        var trade = new Trade
        {
            Player1Id = player1.Id,
            Player2Id = player2.Id,
            TradeDate = DateTimeOffset.UtcNow,
            TradeStatus = TradeStatus.Offered,
            TradePokemons = { tradePokemon1, tradePokemon2 }
        };

        _context.Trades.Add(trade);
        await _context.SaveChangesAsync(cancellationToken);
        
        return trade.Id;
    }
}
