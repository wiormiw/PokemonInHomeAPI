using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Security;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.Events;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Moves.Commands.CreateMove;

[Authorize(Roles = Roles.Administrator)]
public record CreateMoveCommand : IRequest<int>
{
    required public string Name { get; init; }
    
    required public string Type { get; init; }
    
    required public string Category { get; init; }
    
    public int Power { get; init; }
    
    public int Accuracy { get; init; }

    public int Pp { get; init; }
}

public class CreateMoveCommandHandler : IRequestHandler<CreateMoveCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateMoveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateMoveCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Name);
        ArgumentNullException.ThrowIfNull(request.Type);

        var entity = new Move
        {
            Name = request.Name,
            Type = PokemonType.From(request.Type),
            Category = Enum.Parse<MovesType>(request.Category, ignoreCase: true),
            Power = request.Power,
            Accuracy = request.Accuracy,
            Pp = request.Pp,
        };
            
        entity.AddDomainEvent(new MoveCreatedEvent(entity));
        
        _context.Moves.Add(entity);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return entity.Id;
    }
}
