using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class MoveConfiguration : IEntityTypeConfiguration<Move>
{
    public void Configure(EntityTypeBuilder<Move> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(m => m.Type)
            .IsRequired();

        builder.Property(m => m.Power);

        builder.Property(m => m.Accuracy)
            .HasPrecision(3, 0); // Optional, accuracy is 0–100

        builder.Property(m => m.Pp)
            .IsRequired();

        builder.HasMany(m => m.PokemonMoves)
            .WithOne(pm => pm.Move)
            .HasForeignKey(pm => pm.MoveId);
    }
}

