namespace PokemonInHomeAPI.Domain.Constants;

public static class ValidationMessage
{
    public const string RequiredMessage = "'{PropertyName}' must not be empty.";
    public const string UniqueMessage = "'{PropertyName}' must be unique.";
    public const string MaxLength255Message = "'{PropertyName}' exceeds the maximum allowed length of 255.";
    public const string PositiveMessage = "'{PropertyName}' must be positive.";
    public const string MaxValue255Message = "'{PropertyName}' must be less than or equal to 255.";
    public const string UnsupportedTypeMessage = "'{PropertyName}' type not supported.";

    // Pagination
    public const string MinValue1Message = "'{PropertyName}' must be greater than or equal to 1.";

    // Catch Pokemon
    public const string MinPokemonLevelMessage = "Wild Pokemon LVL can't be less than 5";
    public const string MaxPokemonLevelMessage = "Wild Pokemon LVL can't exceed 50";
}

