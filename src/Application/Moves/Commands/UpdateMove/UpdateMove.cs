﻿using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Moves.Commands.UpdateMove;

public record UpdateMoveCommand : IRequest
{
    public int Id { get; init; }
    
    public string? Name { get; init; }
    
    public string? Type { get; init; }
    
    public string? Category { get; init; }
    
    public int? Power { get; init; }
    
    public int? Accuracy { get; init; }

    public int? Pp { get; init; }
}

public class UpdateMoveCommandHandler : IRequestHandler<UpdateMoveCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateMoveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateMoveCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Moves
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);

        if (request.Name is not null)
            entity.Name = request.Name;
        
        if (request.Type is not null)
            entity.Type = PokemonType.From(request.Type);

        if (request.Category is not null)
            entity.Category = Enum.Parse<MovesType>(request.Category, ignoreCase: true);
        
        if (request.Power.HasValue)
            entity.Power = request.Power.Value;
        
        if (request.Accuracy.HasValue)
            entity.Accuracy = request.Accuracy.Value;
        
        if (request.Pp.HasValue)
            entity.Pp = request.Pp.Value;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}
