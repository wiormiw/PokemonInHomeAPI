using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class TradeConfiguration : IEntityTypeConfiguration<Trade>
{
    public void Configure(EntityTypeBuilder<Trade> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TradeDate)
            .IsRequired();

        builder.HasOne(t => t.Player1)
            .WithMany(p => p.Player1Trades)
            .HasForeignKey(t => t.Player1Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Player2)
            .WithMany(p => p.Player2Trades)
            .HasForeignKey(t => t.Player2Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.TradePokemons)
            .WithOne(tp => tp.Trade)
            .HasForeignKey(tp => tp.TradeId);
    }
}
