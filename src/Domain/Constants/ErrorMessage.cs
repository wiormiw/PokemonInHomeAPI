namespace PokemonInHomeAPI.Domain.Constants;

public abstract class ErrorMessage
{
    // Common
    public const string AttributeLengthError = "{0} exceeds the maximum length of {1}.";
    public const string RequiredAttributeError = "{0} is required.";
    public const string EmptyAttributeError = "{0} must not be empty.";
    public const string MustPositiveAttributeError = "{0} must be positive.";
    public const string UniqueAttributeError = "{0} must be unique.";
    public const string LessThanOrEqualToAttributeError = "{0} must be less than or equal to {1}.";
    
    // Pokemon
    public const string PokemonUnsupportedError = "{0} type not supported.";
}
