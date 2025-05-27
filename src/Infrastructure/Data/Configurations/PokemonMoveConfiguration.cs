using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class PokemonMoveConfiguration : IEntityTypeConfiguration<PokemonMove>
{
    public void Configure(EntityTypeBuilder<PokemonMove> builder)
    {
        builder.HasKey(pm => new { pm.PokemonId, pm.MoveId });

        builder.Property(pm => pm.Slot)
            .IsRequired();

        builder.Property(pm => pm.CurrentPp)
            .IsRequired();

        builder.HasOne(pm => pm.Pokemon)
            .WithMany(p => p.PokemonMoves)
            .HasForeignKey(pm => pm.PokemonId);

        builder.HasOne(pm => pm.Move)
            .WithMany(m => m.PokemonMoves)
            .HasForeignKey(pm => pm.MoveId);
    }
}

