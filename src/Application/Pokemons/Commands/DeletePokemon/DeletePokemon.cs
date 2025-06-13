using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Security;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.Events;

namespace PokemonInHomeAPI.Application.Pokemons.Commands.DeletePokemon;

[Authorize(Roles = Roles.Administrator)]
public record DeletePokemonCommand(int Id) : IRequest;

public class DeletePokemonCommandHandler : IRequestHandler<DeletePokemonCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePokemonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePokemonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PokemonSpecies
            .FindAsync(new object[] { request.Id }, cancellationToken);
        
        Guard.Against.NotFound(request.Id, entity);
        
        _context.PokemonSpecies.Remove(entity);

        entity.AddDomainEvent(new PokemonDeletedEvent(entity));

        await _context.SaveChangesWithEventsAsync(cancellationToken);
    }
}
