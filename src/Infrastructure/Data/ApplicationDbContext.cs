using System.Reflection;
using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MediatR;
using PokemonInHomeAPI.Domain.Common;

namespace PokemonInHomeAPI.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
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
    public new DatabaseFacade Database => base.Database;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public async Task<int> SaveChangesWithEventsAsync(CancellationToken cancellationToken)
    {
        await DispatchDomainEventAsync();

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task DispatchDomainEventAsync()
    {
        var domainEntities = ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        // Clear domain events
        domainEntities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
