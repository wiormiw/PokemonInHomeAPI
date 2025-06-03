namespace PokemonInHomeAPI.Domain.Entities;

public class Move : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    
    public PokemonType Type { get; set; } = PokemonType.Unknown;
    
    public int Power { get; set; }
    
    public int Accuracy { get; set; }

    public int Pp { get; set; }
    
    public ICollection<PokemonMove> PokemonMoves { get; set; } = new List<PokemonMove>();
}
