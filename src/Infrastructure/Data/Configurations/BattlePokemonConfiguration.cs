using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class BattlePokemonConfiguration : IEntityTypeConfiguration<BattlePokemon>
{
    public void Configure(EntityTypeBuilder<BattlePokemon> builder)
    {
        builder.HasKey(bp => new { bp.BattleId, bp.PokemonId });

        builder.Property(bp => bp.PlayerId)
            .IsRequired();

        builder.HasOne(bp => bp.Battle)
            .WithMany(b => b.BattlePokemons)
            .HasForeignKey(bp => bp.BattleId);

        builder.HasOne(bp => bp.Pokemon)
            .WithMany(p => p.BattlePokemons)
            .HasForeignKey(bp => bp.PokemonId);

        builder.HasOne(bp => bp.Player)
            .WithMany()
            .HasForeignKey(bp => bp.PlayerId);
    }
}
