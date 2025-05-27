using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Infrastructure.Identity;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Created)
            .IsRequired();

        builder.HasMany(p => p.PlayerPokemons)
            .WithOne(pp => pp.Player)
            .HasForeignKey(pp => pp.PlayerId);
        
        builder.HasMany(p => p.Pokedexes)
            .WithOne(pd => pd.Player)
            .HasForeignKey(pd => pd.PlayerId);

        builder.HasMany(p => p.PlayerItems)
            .WithOne(pi => pi.Player)
            .HasForeignKey(pi => pi.PlayerId);
        
        builder.HasMany(p => p.Player1Battles)
            .WithOne(b => b.Player1)
            .HasForeignKey(b => b.Player1Id)
            .OnDelete(DeleteBehavior.Restrict); // Avoid cascade cycles

        builder.HasMany(p => p.Player2Battles)
            .WithOne(b => b.Player2)
            .HasForeignKey(b => b.Player2Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Player1Trades)
            .WithOne(t => t.Player1)
            .HasForeignKey(t => t.Player1Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Player2Trades)
            .WithOne(t => t.Player2)
            .HasForeignKey(t => t.Player2Id)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne<ApplicationUser>()
            .WithOne(u => u.Player)
            .HasForeignKey<Player>(p => p.ApplicationUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
