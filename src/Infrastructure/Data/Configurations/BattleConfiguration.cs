using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class BattleConfiguration : IEntityTypeConfiguration<Battle>
{
    public void Configure(EntityTypeBuilder<Battle> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.BattleDate)
            .IsRequired();

        builder.HasOne(b => b.Player1)
            .WithMany(p => p.Player1Battles)
            .HasForeignKey(b => b.Player1Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Player2)
            .WithMany(p => p.Player2Battles)
            .HasForeignKey(b => b.Player2Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Winner)
            .WithMany()
            .HasForeignKey(b => b.WinnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.BattlePokemons)
            .WithOne(bp => bp.Battle)
            .HasForeignKey(bp => bp.BattleId);
    }
}

