namespace PokemonInHomeAPI.Domain.Entities;

public class Item : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public ItemType Type { get; set; }
    
    public ICollection<PlayerItem> PlayerItems { get; set; } = new List<PlayerItem>();
}
