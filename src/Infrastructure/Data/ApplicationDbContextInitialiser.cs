using PokemonInHomeAPI.Domain.Constants;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Infrastructure.Identity;
using PokemonInHomeAPI.Infrastructure.Helper.PokemonHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static void AddAsyncSeeding(this DbContextOptionsBuilder builder, IServiceProvider serviceProvider)
    {
        builder.UseAsyncSeeding(async (context, _, ct) =>
        {
            var initialiser = serviceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.SeedAsync();
        });
    }

    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var userRole = new IdentityRole(Roles.User);
        var administratorRole = new IdentityRole(Roles.Administrator);
        var playerRole = new IdentityRole(Roles.Player);

        if (_roleManager.Roles.All(r => r.Name != userRole.Name))
        {
            await _roleManager.CreateAsync(userRole);
        }
        
        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        if (_roleManager.Roles.All(r => r.Name != playerRole.Name))
        {
            await _roleManager.CreateAsync(playerRole);
        }
        
        // Pokemon species seeding
        if (!_context.PokemonSpecies.Any())
        {
            var species = new List<PokemonSpecies>
            {
                new PokemonSpecies
                {
                    Name = "Bulbasaur",
                    Type1 = PokemonType.Grass,
                    Type2 = PokemonType.Poison,
                    BaseHp = 45,
                    BaseAttack = 49,
                    BaseDefense = 49,
                    BaseSpecialAttack = 65,
                    BaseSpecialDefense = 65,
                    BaseSpeed = 45
                },
                new PokemonSpecies
                {
                    Name = "Charmander",
                    Type1 = PokemonType.Fire,
                    BaseHp = 39,
                    BaseAttack = 52,
                    BaseDefense = 43,
                    BaseSpecialAttack = 60,
                    BaseSpecialDefense = 50,
                    BaseSpeed = 65
                },
                new PokemonSpecies
                {
                    Name = "Squirtle",
                    Type1 = PokemonType.Water,
                    BaseHp = 44,
                    BaseAttack = 48,
                    BaseDefense = 65,
                    BaseSpecialAttack = 50,
                    BaseSpecialDefense = 64,
                    BaseSpeed = 43
                },
                new PokemonSpecies
                {
                    Name = "Pikachu",
                    Type1 = PokemonType.Electric,
                    BaseHp = 35,
                    BaseAttack = 55,
                    BaseDefense = 40,
                    BaseSpecialAttack = 50,
                    BaseSpecialDefense = 50,
                    BaseSpeed = 90
                }
            };

            _context.PokemonSpecies.AddRange(species);
            await _context.SaveChangesAsync();
        }

        // Default users
        var administrator = new ApplicationUser 
            { UserName = "administrator@localhost", Email = "administrator@localhost" };
        var player = new ApplicationUser 
            { UserName = "player@localhost", Email = "player@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(userRole.Name) && !string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] {  userRole.Name, administratorRole.Name });
            }
        }
        
        if (_userManager.Users.All(u => u.UserName != player.UserName))
        {
            await _userManager.CreateAsync(player, "Player1!");
            if (!string.IsNullOrWhiteSpace(userRole.Name) && !string.IsNullOrWhiteSpace(playerRole.Name))
            {
                var playerData = new Player { ApplicationUserId = player.Id};
            
                // Attach Player as Extended Identity User
                await _context.Players.AddAsync(playerData);
                await _context.SaveChangesAsync();
                
                // Attach Empty Pokedex with player
                await PokemonHelperInitializer.AddEmptyPokedexForPlayerAsync(_context, playerData.Id);
                await _context.SaveChangesAsync();
                
                await _userManager.AddToRolesAsync(player, new[] {  userRole.Name, playerRole.Name });
            }
            
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯" },
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }
    }
}
