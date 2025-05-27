using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(i => i.Description); // Optional

        builder.Property(i => i.Type)
            .IsRequired();

        builder.HasMany(i => i.PlayerItems)
            .WithOne(pi => pi.Item)
            .HasForeignKey(pi => pi.ItemId);
    }
}

