namespace PokemonInHomeAPI.Domain.Entities;

public class BattlePokemon
{
    public int BattleId { get; set; }
    
    public int PokemonId { get; set; }
    
    public int PlayerId { get; set; }

    public Battle? Battle { get; set; }
    
    public Pokemon? Pokemon { get; set; }
    
    public Player? Player { get; set; }
}
