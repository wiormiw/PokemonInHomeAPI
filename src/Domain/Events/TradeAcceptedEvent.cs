namespace PokemonInHomeAPI.Domain.Events;

public class TradeAcceptedEvent : BaseEvent
{
    public int TradeId { get; }
    public List<int> AffectedPokemonIds { get; }

    public TradeAcceptedEvent(int tradeId, List<int> affectedPokemonIds)
    {
        TradeId = tradeId;
        AffectedPokemonIds = affectedPokemonIds;
    }
}
