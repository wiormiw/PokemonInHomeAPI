namespace PokemonInHomeAPI.Domain.Entities;

public class Battle : BaseAuditableEntity
{
    public int Player1Id { get; set; }
    
    public int Player2Id { get; set; }
    
    public int? WinnerId { get; set; }
    
    public DateTimeOffset BattleDate { get; set; } = DateTimeOffset.Now;
    
    public Player? Player1 { get; set; }
    
    public Player? Player2 { get; set; }
    
    public Player? Winner { get; set; }
    
    public ICollection<BattlePokemon> BattlePokemons { get; set; } = new List<BattlePokemon>();
}
