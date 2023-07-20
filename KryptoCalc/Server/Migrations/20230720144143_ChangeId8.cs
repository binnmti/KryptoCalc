using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KryptoCalc.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeId8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceId",
                table: "Price",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "CoinMarketsId",
                table: "Price",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinMarketsId",
                table: "Price");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Price",
                newName: "PriceId");
        }
    }
}
