using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Security;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.UpdatePokemon;

[Authorize(Roles = Roles.Administrator)]
public record UpdatePokemonCommand : IRequest
{
    public int Id { get; init; }
    
    public string? Name { get; init; }
    
    public string? Type1 { get; init; }
    
    public string? Type2 { get; init; }
   
    public int? BaseHp { get; init; }
    
    public int? BaseAttack { get; init; }
    
    public int? BaseDefense { get; init; }
    
    public int? BaseSpecialAttack { get; init; }
    
    public int? BaseSpecialDefense { get; init; }
    
    public int? BaseSpeed { get; init; }
}

public class UpdatePokemonCommandHandler : IRequestHandler<UpdatePokemonCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePokemonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePokemonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PokemonSpecies
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        
        if (request.Name is not null)
            entity.Name = request.Name;

        if (request.Type1 is not null)
            entity.Type1 = PokemonType.From(request.Type1);

        if (request.Type2 is not null)
            entity.Type2 = PokemonType.From(request.Type2);
        else if (request.Type2 == null)
            entity.Type2 = null;

        if (request.BaseHp.HasValue)
            entity.BaseHp = request.BaseHp.Value;

        if (request.BaseAttack.HasValue)
            entity.BaseAttack = request.BaseAttack.Value;

        if (request.BaseDefense.HasValue)
            entity.BaseDefense = request.BaseDefense.Value;

        if (request.BaseSpecialAttack.HasValue)
            entity.BaseSpecialAttack = request.BaseSpecialAttack.Value;

        if (request.BaseSpecialDefense.HasValue)
            entity.BaseSpecialDefense = request.BaseSpecialDefense.Value;

        if (request.BaseSpeed.HasValue)
            entity.BaseSpeed = request.BaseSpeed.Value;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
