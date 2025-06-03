using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Pokemons.Queries.GetPokemons;

public class PokemonDto
{
    public string Name { get; init; } = string.Empty;

    public PokemonType Type1 { get; init; } = PokemonType.Unknown;
    
    public PokemonType? Type2 { get; init; }
    
    public int BaseHp { get; init; }
    
    public int BaseAttack { get; init; }
    
    public int BaseDefense { get; init; }
    
    public int BaseSpeed { get; init; }
    
    public int BaseSpecialAttack { get; init; }
    
    public int BaseSpecialDefense { get; init; }
    
    // TODO: Exposing the entity, change when the functionality already created!
    public ICollection<Pokemon> Pokemons { get; init; } = new List<Pokemon>();
    
    // TODO: Exposing the entity, change when the functionality already created!
    public ICollection<Pokedex> Pokedexes { get; init; } = new List<Pokedex>();
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PokemonSpecies, PokemonDto>();
        }
    }
}
