using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Pokemons.Queries.GetPokemons;

public class PokemonDto
{
    public int Id { get; init; }
    
    public PokemonType? Type1 { get; init; }
    
    public PokemonType? Type2 { get; init; }
    
    public int BaseId { get; init; }
    
    public int BaseAttack { get; init; }
    
    public int BaseDefense { get; init; }
    
    public int BaseSpeed { get; init; }
    
    public int BaseSpecialAttack { get; init; }
    
    public int BaseSpecialDefense { get; init; }
    
    public ICollection<Pokemon> Pokemons { get; init; } = new List<Pokemon>();
    
    public ICollection<Pokedex> Pokedex { get; init; } = new List<Pokedex>();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PokemonSpecies, PokemonDto>();
        }
    }
}
