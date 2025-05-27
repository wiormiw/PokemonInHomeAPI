using System.Reflection;
using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PokemonInHomeAPI.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<PokemonSpecies> PokemonSpecies => Set<PokemonSpecies>();
    public DbSet<Pokemon> Pokemons => Set<Pokemon>();
    public DbSet<PlayerPokemon> PlayerPokemons => Set<PlayerPokemon>();
    public DbSet<Pokedex> Pokedexes => Set<Pokedex>();
    public DbSet<Move> Moves => Set<Move>();
    public DbSet<PokemonMove> PokemonMoves => Set<PokemonMove>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<PlayerItem> PlayerItems => Set<PlayerItem>();
    public DbSet<Battle> Battles => Set<Battle>();
    public DbSet<BattlePokemon> BattlePokemons => Set<BattlePokemon>();
    public DbSet<Trade> Trades => Set<Trade>();
    public DbSet<TradePokemon> TradePokemons => Set<TradePokemon>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
