using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class PlayerPokemonConfiguration : IEntityTypeConfiguration<PlayerPokemon>
{
    public void Configure(EntityTypeBuilder<PlayerPokemon> builder)
    {
        builder.HasKey(pp => new { pp.PlayerId, pp.PokemonId });

        builder.Property(pp => pp.IsActive)
            .IsRequired();
        
        builder.Property(pp => pp.Nickname)
            .HasMaxLength(50);

        builder.Property(pp => pp.CaughtAt)
            .IsRequired();

        builder.HasOne(pp => pp.Player)
            .WithMany(p => p.PlayerPokemons)
            .HasForeignKey(pp => pp.PlayerId);

        builder.HasOne(pp => pp.Pokemon)
            .WithMany(p => p.PlayerPokemons)
            .HasForeignKey(pp => pp.PokemonId);
    }
}
