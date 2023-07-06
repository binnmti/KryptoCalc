using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KryptoCalc.Server.Data;
using KryptoCalc.Shared;
using System.Linq;

namespace KryptoCalc.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CoinsController : ControllerBase
{
    private readonly KryptoCalcServerContext _context;

    public CoinsController(KryptoCalcServerContext context)
    {
        _context = context;
    }

    [Route("/coinMarkets")]
    public async Task<IEnumerable<CoinMarkets>> CoinMarkets(int page, int count)
    {
        var coinMarkets = new List<CoinMarkets>();
        //TODO:右上には1yen だけでなく入力値も出す
        if (page == 1)
        {
            var coinMarket = CurrencyList.GetCoinMarkets("jpy");
            coinMarkets.Add(coinMarket);
            //coinMarkets.Add(new CoinMarkets("yen", "yen", "Japan", "Japan.png", 1));
        }
        var skip = (Math.Max(page, 1) - 1) * count;
        var take = count == -1 ? int.MaxValue : count;
        coinMarkets.AddRange(GetCoinMarkets(skip, take));
        return coinMarkets;
    }

    [Route("/coinMarketsCount")]
    public async Task<int> CoinMarketsCount()
        => await _context.CoinMarkets.Where(x => x.MarketCapRank != 0).CountAsync();

    private IQueryable<CoinMarkets> GetCoinMarkets(int skip, int take)
        => _context.CoinMarkets.Where(x => x.MarketCapRank != 0)
                            .OrderBy(x => x.MarketCapRank)
                            .Skip(skip)
                            .Take(take)
                            .AsTracking();
}