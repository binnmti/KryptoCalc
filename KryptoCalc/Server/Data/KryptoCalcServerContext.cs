using Microsoft.EntityFrameworkCore;
using KryptoCalc.Shared;

namespace KryptoCalc.Server.Data;

public class KryptoCalcServerContext : DbContext
{
    public KryptoCalcServerContext (DbContextOptions<KryptoCalcServerContext> options)
        : base(options)
    {
    }

    public DbSet<Coin> Coin { get; set; } = default!;
    public DbSet<CoinMarkets> CoinMarkets { get; set; } = default!;
    public DbSet<Price> Price { get; set; } = default!;
}
