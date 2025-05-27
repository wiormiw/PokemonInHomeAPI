using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokemonInHomeAPI.Domain.Entities;

public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Level)
            .IsRequired();

        builder.Property(p => p.IvHp).IsRequired();
        builder.Property(p => p.IvAttack).IsRequired();
        builder.Property(p => p.IvDefense).IsRequired();
        builder.Property(p => p.IvSpecialAttack).IsRequired();
        builder.Property(p => p.IvSpecialDefense).IsRequired();
        builder.Property(p => p.IvSpeed).IsRequired();

        builder.Property(p => p.CurrentHp).IsRequired();

        builder.HasOne(p => p.Species)
            .WithMany(s => s.Pokemons)
            .HasForeignKey(p => p.SpeciesId)
            .IsRequired();

        // Configure collections (optional)
        builder.HasMany(p => p.PlayerPokemons)
            .WithOne(pp => pp.Pokemon)
            .HasForeignKey(pp => pp.PokemonId);

        builder.HasMany(p => p.PokemonMoves)
            .WithOne(pm => pm.Pokemon)
            .HasForeignKey(pm => pm.PokemonId);

        builder.HasMany(p => p.BattlePokemons)
            .WithOne(bp => bp.Pokemon)
            .HasForeignKey(bp => bp.PokemonId);

        builder.HasMany(p => p.TradePokemons)
            .WithOne(tp => tp.Pokemon)
            .HasForeignKey(tp => tp.PokemonId);
    }
}
