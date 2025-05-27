namespace PokemonInHomeAPI.Domain.Entities;

public class PlayerItem
{
    public int PlayerId { get; set; }
    
    public int ItemId { get; set; } 
    
    public int Quantity { get; set; }

    public Player? Player { get; set; }
    
    public Item? Item { get; set; }
}
