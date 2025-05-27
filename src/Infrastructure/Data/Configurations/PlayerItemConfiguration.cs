using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class PlayerItemConfiguration : IEntityTypeConfiguration<PlayerItem>
{
    public void Configure(EntityTypeBuilder<PlayerItem> builder)
    {
        builder.HasKey(pi => new { pi.PlayerId, pi.ItemId });

        builder.Property(pi => pi.Quantity)
            .IsRequired();

        builder.HasOne(pi => pi.Player)
            .WithMany(p => p.PlayerItems)
            .HasForeignKey(pi => pi.PlayerId);

        builder.HasOne(pi => pi.Item)
            .WithMany(i => i.PlayerItems)
            .HasForeignKey(pi => pi.ItemId);
    }
}

