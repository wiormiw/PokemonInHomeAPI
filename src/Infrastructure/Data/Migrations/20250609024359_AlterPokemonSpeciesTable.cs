using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonInHomeAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterPokemonSpeciesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvAttack",
                table: "Pokemons",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvAttack",
                table: "Pokemons");
        }
    }
}
