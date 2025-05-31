using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Player> Players { get; }

    DbSet<PokemonSpecies> PokemonSpecies { get; }

    DbSet<Pokemon> Pokemons { get; }

    DbSet<PlayerPokemon> PlayerPokemons { get; }

    DbSet<Pokedex> Pokedexes { get; }

    DbSet<Move> Moves { get; }

    DbSet<PokemonMove> PokemonMoves { get; }

    DbSet<Item> Items { get; }

    DbSet<PlayerItem> PlayerItems { get; }

    DbSet<Battle> Battles { get; }
    
    DbSet<BattlePokemon> BattlePokemons { get; }
    
    DbSet<Trade> Trades { get; }
    
    DbSet<TradePokemon> TradePokemons { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
