namespace PokemonInHomeAPI.Domain.Entities;

public class Pokemon : BaseAuditableEntity
{
    public int SpeciesId { get; set; }
    
    public int Level { get; set; }
    
    public int IvHp { get; set; }
    
    public int IvAttack { get; set; }
    
    public int IvDefense { get; set; }
    
    public int IvSpeed { get; set; }
    
    public int IvSpecialAttack { get; set; }
    
    public int IvSpecialDefense { get; set; }
    
    public int EvHp { get; set; }
    
    public int EvDefense { get; set; }
    
    public int EvSpecialAttack { get; set; }
    
    public int EvSpecialDefense { get; set; }
    
    public int EvSpeed { get; set; }
    
    public int CurrentHp { get; set; }
    
    public PokemonSpecies? Species { get; set; }
    
    public ICollection<PlayerPokemon> PlayerPokemons { get; set; } = new List<PlayerPokemon>();
    public ICollection<PokemonMove> PokemonMoves { get; set; } = new List<PokemonMove>();
    public ICollection<BattlePokemon> BattlePokemons { get; set; } = new List<BattlePokemon>();
    public ICollection<TradePokemon> TradePokemons { get; set; } = new List<TradePokemon>();
}
