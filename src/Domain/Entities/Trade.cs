namespace PokemonInHomeAPI.Domain.Entities;

public class Trade : BaseAuditableEntity
{
    public int Player1Id { get; set; }
    
    public int Player2Id { get; set; }
    
    public DateTimeOffset TradeDate { get; set; } = DateTimeOffset.Now;

    public Player? Player1 { get; set; }

    public Player? Player2 { get; set; }
    
    public ICollection<TradePokemon> TradePokemons { get; set; } = new List<TradePokemon>();
}
