namespace PokemonInHomeAPI.Domain.Entities;

public class PokemonSpecies : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public PokemonType Type1 { get; set; } = new PokemonType("Unknown");
    
    public PokemonType? Type2 { get; set; }
    
    public int BaseHp { get; set; }
    
    public int BaseAttack { get; set; }
    
    public int BaseDefense { get; set; }
    
    public int BaseSpeed { get; set; }
    
    public int BaseSpecialAttack { get; set; }
    
    public int BaseSpecialDefense { get; set; }
    
    public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
    public ICollection<Pokedex> Pokedexes { get; set; } = new List<Pokedex>();
}
