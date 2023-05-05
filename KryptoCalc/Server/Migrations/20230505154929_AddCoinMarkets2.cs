using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KryptoCalc.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddCoinMarkets2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinMarkets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPrice = table.Column<float>(type: "real", nullable: false),
                    MarketCap = table.Column<long>(type: "bigint", nullable: false),
                    MarketCapRank = table.Column<int>(type: "int", nullable: false),
                    FullyDilutedValuation = table.Column<long>(type: "bigint", nullable: false),
                    TotalVolume = table.Column<long>(type: "bigint", nullable: false),
                    High24h = table.Column<float>(type: "real", nullable: false),
                    Low24h = table.Column<float>(type: "real", nullable: false),
                    PriceChange24h = table.Column<float>(type: "real", nullable: false),
                    PriceChangePercentage24h = table.Column<float>(type: "real", nullable: false),
                    MarketCapChange24h = table.Column<float>(type: "real", nullable: false),
                    MarketCapChangePercentage24h = table.Column<float>(type: "real", nullable: false),
                    CirculatingSupply = table.Column<float>(type: "real", nullable: false),
                    TotalSupply = table.Column<float>(type: "real", nullable: false),
                    MaxSupply = table.Column<float>(type: "real", nullable: false),
                    Ath = table.Column<float>(type: "real", nullable: false),
                    AthChangePercentage = table.Column<float>(type: "real", nullable: false),
                    AthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Atl = table.Column<float>(type: "real", nullable: false),
                    AtlChangePercentage = table.Column<float>(type: "real", nullable: false),
                    AtlDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinMarkets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinMarkets");
        }
    }
}
