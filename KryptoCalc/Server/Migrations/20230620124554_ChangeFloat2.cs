using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KryptoCalc.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFloat2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "FullyDilutedValuation",
                table: "CoinMarkets",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FullyDilutedValuation",
                table: "CoinMarkets",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
