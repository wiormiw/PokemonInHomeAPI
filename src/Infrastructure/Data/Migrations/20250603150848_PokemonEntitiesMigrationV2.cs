using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonInHomeAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class PokemonEntitiesMigrationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Power",
                table: "Moves",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Accuracy",
                table: "Moves",
                type: "integer",
                precision: 3,
                scale: 0,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldPrecision: 3,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Power",
                table: "Moves",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Accuracy",
                table: "Moves",
                type: "integer",
                precision: 3,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldPrecision: 3,
                oldScale: 0);
        }
    }
}
