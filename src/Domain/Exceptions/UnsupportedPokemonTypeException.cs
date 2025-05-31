namespace PokemonInHomeAPI.Domain.Exceptions;

public class UnsupportedPokemonTypeException : Exception
{
    
    public UnsupportedPokemonTypeException(string pokemonType)
        : base($"Pokemon type \"{pokemonType}\" is not exists.")
    {
    }
    
}
