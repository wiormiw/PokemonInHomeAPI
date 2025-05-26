namespace PokemonInHomeAPI.Domain.Constants;

public abstract class Policies
{
    //Default Policy
    public const string User = nameof(User);
    public const string Administrator = nameof(Administrator);
    public const string Player = nameof(Player);
    
    //Specific Policy
    public const string CanPurge = nameof(CanPurge);
}
