namespace PokemonInHomeAPI.Domain.ValueObjects;

public class PokemonType(string name) : ValueObject
{
    public static PokemonType From(string name)
    {
        var pokemonType = new PokemonType(name);

        if (!SupportedTypes.Contains(pokemonType))
        {
            throw new UnsupportedPokemonTypeException(name);
        }

        return pokemonType;
    }

    public static PokemonType Normal => new("Normal");
    public static PokemonType Fire => new("Fire");
    public static PokemonType Water => new("Water");
    public static PokemonType Grass => new("Grass");
    public static PokemonType Electric => new("Electric");
    public static PokemonType Ice => new("Ice");
    public static PokemonType Fighting => new("Fighting");
    public static PokemonType Poison => new("Poison");
    public static PokemonType Ground => new("Ground");
    public static PokemonType Flying => new("Flying");
    public static PokemonType Psychic => new("Psychic");
    public static PokemonType Bug => new("Bug");
    public static PokemonType Rock => new("Rock");
    public static PokemonType Ghost => new("Ghost");
    public static PokemonType Dragon => new("Dragon");
    public static PokemonType Dark => new("Dark");
    public static PokemonType Steel => new("Steel");
    public static PokemonType Fairy => new("Fairy");

    public string Name { get; private set; } = string.IsNullOrWhiteSpace(name) ? "Unknown" : name;

    public static implicit operator string(PokemonType pokemonType)
    {
        return pokemonType.ToString();
    }

    public static explicit operator PokemonType(string name)
    {
        return From(name);
    }

    public override string ToString()
    {
        return Name;
    }

    protected static IEnumerable<PokemonType> SupportedTypes
    {
        get
        {
            yield return Normal;
            yield return Fire;
            yield return Water;
            yield return Grass;
            yield return Electric;
            yield return Ice;
            yield return Fighting;
            yield return Poison;
            yield return Ground;
            yield return Flying;
            yield return Psychic;
            yield return Bug;
            yield return Rock;
            yield return Ghost;
            yield return Dragon;
            yield return Dark;
            yield return Steel;
            yield return Fairy;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}

public class UnsupportedPokemonTypeException(string typeName) : Exception
{
    public override string Message => $"The Pokémon type '{typeName}' is not supported.";
}
