using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class PokemonSpeciesConfiguration : IEntityTypeConfiguration<PokemonSpecies>
{
    public void Configure(EntityTypeBuilder<PokemonSpecies> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.OwnsOne(p => p.Type1, type =>
        {
            type.Property(t => t.Name)
                .IsRequired()
                .HasColumnType("text")
                .IsRequired();
        });
        
        builder.OwnsOne(p => p.Type2, type =>
        {
            type.Property(t => t.Name)
                .HasColumnType("text");
        });

        builder.Property(p => p.BaseHp)
            .IsRequired();

        builder.Property(p => p.BaseAttack)
            .IsRequired();

        builder.Property(p => p.BaseDefense)
            .IsRequired();

        builder.Property(p => p.BaseSpecialAttack)
            .IsRequired();

        builder.Property(p => p.BaseSpecialDefense)
            .IsRequired();

        builder.Property(p => p.BaseSpeed)
            .IsRequired();
        
        builder.HasMany(p => p.Pokemons)
            .WithOne(p => p.Species)
            .HasForeignKey(p => p.SpeciesId);

        builder.HasMany(p => p.Pokedexes)
            .WithOne(p => p.Species)
            .HasForeignKey(p => p.SpeciesId);
    }
}

