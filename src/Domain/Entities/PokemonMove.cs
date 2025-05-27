namespace PokemonInHomeAPI.Domain.Entities;

public class PokemonMove
{
    public int PokemonId { get; set; }
    
    public int MoveId { get; set; }
    
    public int Slot { get; set; }
    
    public int CurrentPp { get; set; }

    public Pokemon? Pokemon { get; set; }
    
    public Move? Move { get; set; }
}
