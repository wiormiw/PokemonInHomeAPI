namespace PokemonInHomeAPI.Domain.Events;

public class MoveCreatedEvent : BaseEvent
{
    public MoveCreatedEvent(Move move)
    {
        Move = move;
    }

    public Move Move { get; }
}
