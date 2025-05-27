using Microsoft.AspNetCore.Identity;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public Player? Player { get; set; } 
}
