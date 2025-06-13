namespace PokemonInHomeAPI.Domain.Events;

public class PokemonCaughtEvent : BaseEvent
{
    public PokemonCaughtEvent(int speciesId, int playerId)
    {
        SpeciesId = speciesId;
        PlayerId = playerId;
    }

    public int SpeciesId { get; }
    public int PlayerId { get; }
}
