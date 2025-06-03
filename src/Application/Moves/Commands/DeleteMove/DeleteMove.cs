using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Events;

namespace PokemonInHomeAPI.Application.Moves.Commands.DeleteMove;

public record DeleteMoveCommand(int Id) : IRequest;

public class DeleteMoveCommandHandler : IRequestHandler<DeleteMoveCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteMoveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteMoveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Moves
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        
        _context.Moves.Remove(entity);

        entity.AddDomainEvent(new MoveDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
