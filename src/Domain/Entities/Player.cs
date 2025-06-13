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

    public PlayerPokemon CatchPokemon(PokemonSpecies species, Pokemon wildPokemon, string nickname)
    {
        var playerPokemon = new PlayerPokemon
        {
            PlayerId = this.Id,
            Pokemon = wildPokemon,
            Nickname = nickname,
            CaughtAt = DateTimeOffset.UtcNow,
            IsActive = false,
        };

        this.PlayerPokemons.Add(playerPokemon);

        this.AddDomainEvent(new PokemonCaughtEvent(species.Id, this.Id));

        return playerPokemon;
    }
}
