namespace PokemonInHomeAPI.Domain.Events;

public class PokemonCreatedEvent : BaseEvent
{
    public PokemonCreatedEvent(PokemonSpecies pokemon)
    {
        Pokemon = pokemon;
    }
    
    public PokemonSpecies Pokemon { get; }
}
