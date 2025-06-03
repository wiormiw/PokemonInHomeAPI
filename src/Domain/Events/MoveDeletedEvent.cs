namespace PokemonInHomeAPI.Domain.Events;

public class MoveDeletedEvent : BaseEvent
{
    public MoveDeletedEvent(Move move)
    {
        Move = move;
    }
    
    public Move Move { get; }
}
