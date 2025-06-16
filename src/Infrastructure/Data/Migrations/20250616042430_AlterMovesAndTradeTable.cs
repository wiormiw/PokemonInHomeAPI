using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonInHomeAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterMovesAndTradeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TradeStatus",
                table: "Trades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Moves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Effect",
                table: "Moves",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradeStatus",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "Effect",
                table: "Moves");
        }
    }
}
