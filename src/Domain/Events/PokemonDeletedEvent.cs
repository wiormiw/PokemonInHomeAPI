namespace PokemonInHomeAPI.Domain.Events;

public class PokemonDeletedEvent : BaseEvent
{
    public PokemonDeletedEvent(PokemonSpecies pokemon)
    {
        Pokemon = pokemon;
    }
    
    public PokemonSpecies Pokemon { get; }
}
