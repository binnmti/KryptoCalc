﻿// <auto-generated />
using System;
using KryptoCalc.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KryptoCalc.Server.Migrations
{
    [DbContext(typeof(KryptoCalcServerContext))]
    [Migration("20230720142024_ChangeId7")]
    partial class ChangeId7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KryptoCalc.Shared.Coin", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(20,4)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Coin");
                });

            modelBuilder.Entity("KryptoCalc.Shared.CoinMarkets", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Ath")
                        .HasColumnType("real");

                    b.Property<float>("AthChangePercentage")
                        .HasColumnType("real");

                    b.Property<DateTime>("AthDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Atl")
                        .HasColumnType("real");

                    b.Property<float>("AtlChangePercentage")
                        .HasColumnType("real");

                    b.Property<DateTime>("AtlDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("CirculatingSupply")
                        .HasColumnType("real");

                    b.Property<float>("CurrentPrice")
                        .HasColumnType("real");

                    b.Property<float>("FullyDilutedValuation")
                        .HasColumnType("real");

                    b.Property<float>("High24h")
                        .HasColumnType("real");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<float>("Low24h")
                        .HasColumnType("real");

                    b.Property<float>("MarketCap")
                        .HasColumnType("real");

                    b.Property<float>("MarketCapChange24h")
                        .HasColumnType("real");

                    b.Property<float>("MarketCapChangePercentage24h")
                        .HasColumnType("real");

                    b.Property<int>("MarketCapRank")
                        .HasColumnType("int");

                    b.Property<float>("MaxSupply")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("PriceChange24h")
                        .HasColumnType("real");

                    b.Property<float>("PriceChangePercentage24h")
                        .HasColumnType("real");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("TotalSupply")
                        .HasColumnType("real");

                    b.Property<float>("TotalVolume")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("CoinMarkets");
                });

            modelBuilder.Entity("KryptoCalc.Shared.Price", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriceId"));

                    b.Property<decimal>("Aed")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Ars")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Aud")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Bdt")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Bhd")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Bits")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Bmd")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Brl")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Cad")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Chf")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Clp")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Cny")
                        .HasColumnType("decimal(20,4)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Czk")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Dkk")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Eur")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Gbp")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Hkd")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Huf")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Idr")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Ils")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Inr")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Jpy")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Krw")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Kwd")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Lkr")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Mmk")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Mxn")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Myr")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Ngn")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Nok")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Nzd")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Php")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Pkr")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Pln")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Rub")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Sar")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Sat")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Sek")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Sgd")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Thb")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Try")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Twd")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Uah")
                        .HasColumnType("decimal(20,4)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Usd")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Vef")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Vnd")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Xag")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Xau")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Xdr")
                        .HasColumnType("decimal(20,4)");

                    b.Property<decimal>("Zar")
                        .HasColumnType("decimal(20,4)");

                    b.HasKey("PriceId");

                    b.ToTable("Price");
                });
#pragma warning restore 612, 618
        }
    }
}
