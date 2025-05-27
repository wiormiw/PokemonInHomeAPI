using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class TradePokemonConfiguration : IEntityTypeConfiguration<TradePokemon>
{
    public void Configure(EntityTypeBuilder<TradePokemon> builder)
    {
        builder.HasKey(tp => new { tp.TradeId, tp.PokemonId });

        builder.Property(tp => tp.PlayerId)
            .IsRequired();

        builder.HasOne(tp => tp.Trade)
            .WithMany(t => t.TradePokemons)
            .HasForeignKey(tp => tp.TradeId);

        builder.HasOne(tp => tp.Pokemon)
            .WithMany(p => p.TradePokemons)
            .HasForeignKey(tp => tp.PokemonId);

        builder.HasOne(tp => tp.Player)
            .WithMany()
            .HasForeignKey(tp => tp.PlayerId);
    }
}

