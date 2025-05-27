using System.ComponentModel.DataAnnotations;

namespace PokemonInHomeAPI.Domain.Entities;

public class Player : BaseAuditableEntity
{
    public string ApplicationUserId { get; set; } = string.Empty;
    
    public ICollection<PlayerPokemon> PlayerPokemons { get; set; } = new List<PlayerPokemon>();
    public ICollection<Pokedex> Pokedexes { get; set; } = new List<Pokedex>();
    public ICollection<PlayerItem> PlayerItems { get; set; } = new List<PlayerItem>();
    public ICollection<Battle> Player1Battles { get; set; } = new List<Battle>();
    public ICollection<Battle> Player2Battles { get; set; } = new List<Battle>();
    public ICollection<Trade> Player1Trades { get; set; } = new List<Trade>();
    public ICollection<Trade> Player2Trades { get; set; } = new List<Trade>();
}
