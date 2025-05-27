namespace PokemonInHomeAPI.Domain.Entities;

public class TradePokemon
{
    public int TradeId { get; set; }
    
    public int PokemonId { get; set; }
    
    public int PlayerId { get; set; }

    public Trade? Trade { get; set; }

    public Pokemon? Pokemon { get; set; }
    
    public Player? Player { get; set; }
}
