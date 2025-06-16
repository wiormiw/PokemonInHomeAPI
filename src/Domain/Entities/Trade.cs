namespace PokemonInHomeAPI.Domain.Entities;

public class Trade : BaseAuditableEntity
{
    public int Player1Id { get; set; }
    
    public int Player2Id { get; set; }
    
    public DateTimeOffset TradeDate { get; set; } = DateTimeOffset.Now;

    public TradeStatus TradeStatus { get; set; } = TradeStatus.Offered;

    public Player? Player1 { get; set; }

    public Player? Player2 { get; set; }
    
    public ISet<TradePokemon> TradePokemons { get; set; } = new HashSet<TradePokemon>();
}

public enum TradeStatus
{
    Offered,
    Completed,
    Cancelled,
    Rejected
}
