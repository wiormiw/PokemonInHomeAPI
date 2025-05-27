namespace PokemonInHomeAPI.Domain.Entities;

public class Pokedex : BaseAuditableEntity
{
    public int PlayerId { get; set; }
    
    public int SpeciesId { get; set; }

    public bool Seen { get; set; } = false;

    public bool Caught { get; set; } = false;

    public Player? Player { get; set; }
    
    public PokemonSpecies? Species { get; set; }
}
