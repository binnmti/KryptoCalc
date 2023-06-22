using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KryptoCalc.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Usd = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Aed = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Ars = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Aud = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Bdt = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Bhd = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Bmd = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Brl = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Cad = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Chf = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Clp = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Cny = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Czk = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Dkk = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Eur = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Gbp = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Hkd = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Huf = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Idr = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Ils = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Inr = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Jpy = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Krw = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Kwd = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Lkr = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Mmk = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Mxn = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Myr = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Ngn = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Nok = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Nzd = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Php = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Pkr = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Pln = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Rub = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Sar = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Sek = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Sgd = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Thb = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Try = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Twd = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Uah = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Vef = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Vnd = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Zar = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Xdr = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Xag = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Xau = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Bits = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    Sat = table.Column<decimal>(type: "decimal(20,4)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Price");
        }
    }
}
