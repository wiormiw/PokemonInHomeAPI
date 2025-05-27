using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

namespace PokemonInHomeAPI.Infrastructure.Data.Configurations;

public class PokedexConfiguration: IEntityTypeConfiguration<Pokedex>
{
    public void Configure(EntityTypeBuilder<Pokedex> builder)
    {
        builder.HasKey(pd => new { pd.PlayerId, pd.SpeciesId });
        
        builder.HasOne(pd => pd.Player)
            .WithMany(p => p.Pokedexes)
            .HasForeignKey(pd => pd.PlayerId);
        
        builder.HasOne(pd => pd.Species)
            .WithMany(s => s.Pokedexes)
            .HasForeignKey(pd => pd.SpeciesId);
    }
    
}
