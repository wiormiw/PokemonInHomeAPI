using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Security;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.Events;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.CreatePokemon;

[Authorize(Roles = Roles.Administrator)]
public record CreatePokemonCommand : IRequest<int>
{
    required public string Name { get; init; }
    
    required public string Type1 { get; init; }
    
    public string? Type2 { get; init; }
   
    public int BaseHp { get; init; }
    
    public int BaseAttack { get; init; }
    
    public int BaseDefense { get; init; }
    
    public int BaseSpecialAttack { get; init; }
    
    public int BaseSpecialDefense { get; init; }
    
    public int BaseSpeed { get; init; }
}

public class CreatePokemonCommandHandler : IRequestHandler<CreatePokemonCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreatePokemonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePokemonCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Name);
        ArgumentNullException.ThrowIfNull(request.Type1);
        
        var entity = new PokemonSpecies
        {
            Name = request.Name,
            Type1 = PokemonType.From(request.Type1),
            Type2 = request.Type2 != null ? PokemonType.From(request.Type2) : null,
            BaseHp = request.BaseHp,
            BaseAttack = request.BaseAttack,
            BaseDefense = request.BaseDefense,
            BaseSpecialAttack = request.BaseSpecialAttack,
            BaseSpecialDefense = request.BaseSpecialDefense,
            BaseSpeed = request.BaseSpeed,
        };
        
        entity.AddDomainEvent(new PokemonCreatedEvent(entity));
        
        _context.PokemonSpecies.Add(entity);
        
        await _context.SaveChangesWithEventsAsync(cancellationToken);
        
        return entity.Id;
    }
}
