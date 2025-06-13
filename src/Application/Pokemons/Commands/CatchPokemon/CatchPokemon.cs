using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Security;
using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection.Pokemons.Commands.CatchPokemon;

[Authorize(Roles = Roles.Player)]
public record CatchPokemonCommand : IRequest<int>
{
    public int SpeciesId { get; init; }

    public int Level { get; init; } = PokemonConstants.DefaultLevel;
    
    public required string Nickname { get; init; }
}

public class CatchPokemonCommandHandler : IRequestHandler<CatchPokemonCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;
    private readonly Random _random = new();

    public CatchPokemonCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(CatchPokemonCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.Id;
        if (userId is null)
            throw new UnauthorizedAccessException(ErrorMessage.UnauthorizedErrorMessage);

        var player = await _context.Players
            .FirstOrDefaultAsync(p => p.ApplicationUserId == userId, cancellationToken);

        Guard.Against.NotFound(userId, player);

        var species = await _context.PokemonSpecies
            .FindAsync(new object[] { request.SpeciesId }, cancellationToken);

        Guard.Against.NotFound(request.SpeciesId, species);

        var pokemonIvHp = _random.Next(PokemonConstants.MinIVsValue, PokemonConstants.MaxIVsValue + 1);
        var pokemonEvHp = 0;

        // Generate random IVs stats (0 - 31)
        var wildPokemon = new Pokemon
        {
            SpeciesId = request.SpeciesId,
            Level = request.Level,
            IvHp = pokemonIvHp,
            IvAttack = _random.Next(PokemonConstants.MinIVsValue, PokemonConstants.MaxIVsValue + 1),
            IvDefense = _random.Next(PokemonConstants.MinIVsValue, PokemonConstants.MaxIVsValue + 1),
            IvSpeed = _random.Next(PokemonConstants.MinIVsValue, PokemonConstants.MaxIVsValue + 1),
            IvSpecialAttack = _random.Next(PokemonConstants.MinIVsValue, PokemonConstants.MaxIVsValue + 1),
            IvSpecialDefense = _random.Next(PokemonConstants.MinIVsValue, PokemonConstants.MaxIVsValue + 1),
            EvHp = pokemonEvHp,
            EvAttack = 0,
            EvDefense = 0,
            EvSpeed = 0,
            EvSpecialAttack = 0,
            EvSpecialDefense = 0,
            CurrentHp = CalculateMaxHp(species.BaseHp, request.Level, pokemonIvHp, pokemonEvHp)
        };

        // Assign random moves (up to 4 moves)
        var availableMoves = await _context.Moves
            .Where(m => m.Type == species.Type1 || m.Type == species.Type2)
            .Take(4)
            .ToListAsync(cancellationToken);

        if (availableMoves.Any())
        {
            wildPokemon.PokemonMoves = availableMoves
                .Select((m, i) => new PokemonMove
                {
                    MoveId = m.Id,
                    Slot = i + 1,
                    CurrentPp = m.Pp
                })
                .ToList();
        }

        // Add wild pokemon to the roster (list of pokemon catch in the wild by user)
        var playerPokemon = player.CatchPokemon(species, wildPokemon, request.Nickname);

        await _context.SaveChangesWithEventsAsync(cancellationToken);
        return wildPokemon.Id;
    }

    private static int CalculateMaxHp(int baseHp, int level, int iv, int ev)
    {
        return (int)(((2 * baseHp + iv + (ev / 4)) * level / 100) + level + 10);
    }
}
