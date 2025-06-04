namespace PokemonInHomeAPI.Domain.Constants;

public static class PokemonConstants
{
    public const int DefaultLevel = 5; // Constants for fallback if the level not provided by the client
    public const int MinWildLevel = 2; // Minimum wild pokemon level can be encountered
    public const int MaxWildLevel = 50; // Maximum wild pokemon level can be encountered
    public const int MinIVsValue = 0; // Minimum IVs stats
    public const int MaxIVsValue = 31; // Maximum IVs stats
}
