namespace PokemonInHomeAPI.Domain.Entities;

public class PlayerPokemon : BaseAuditableEntity
{
    public int PlayerId { get; set; }
    
    public int PokemonId { get; set; }
    
    public bool IsActive { get; set; }

    public string Nickname { get; set; } = string.Empty;
    
    public DateTimeOffset CaughtAt { get; set; } = DateTime.UtcNow;
    
    public Player? Player { get; set; }
    
    public Pokemon? Pokemon { get; set; }
}
