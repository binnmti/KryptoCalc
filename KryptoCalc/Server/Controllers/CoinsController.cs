using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KryptoCalc.Server.Data;
using KryptoCalc.Shared;
using System.Runtime.CompilerServices;

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
    public IEnumerable<CoinMarketView> CoinMarkets(int page, int count, string id)
    {
        var coinMarkets = new List<CoinMarketView>();
        //1ページ目だけ法定通貨を追加
        if (page == 1)
        {
            var coinMarket = LegalCurrency.GetCoinMarketView(id);
            coinMarkets.Add(coinMarket);
        }
        var skip = (Math.Max(page, 1) - 1) * count;
        var take = count == -1 ? int.MaxValue : count;
        coinMarkets.AddRange(GetCoinMarkets(skip, take, id).ToCoinMarketViews());
        return coinMarkets;
    }

    [Route("/coinMarketsCount")]
    public async Task<int> CoinMarketsCount()
        => await _context.CoinMarkets.Where(x => x.MarketCapRank != 0).CountAsync();

    [Route("/exchange")]
    public async Task<decimal> Exchange(string srcCurrency, string dstCurrency)
    {
        var price = await _context.Price.Where(x => x.CoinMarketsId == "bitcoin").OrderBy(x => x.UpdateTime).LastAsync();
        var srcPrice = GetPrice(price, srcCurrency);
        var dstPrice = GetPrice(price, dstCurrency);
        return dstPrice / srcPrice;
    }

    private decimal GetPrice(Price price, string id)
    {
        var idc = "";
        for (int i = 0; i < id.Length; i++)
        {
            idc += i == 0 ? char.ToUpper(id[i]) : char.ToLower(id[i]);
        }
        var value = typeof(Price)?.GetProperty(idc)?.GetValue(price);
        return value != null ? (decimal)value : 0;
    }

    private IQueryable<CoinMarkets> GetCoinMarkets(int skip, int take, string id)
    {
        //Price.{id}の部分がメソッド式に出来ないの理由
        var query = @$"select
	                    CoinMarkets.Id,
	                    CoinMarkets.Symbol,
	                    CoinMarkets.Name,
	                    CoinMarkets.Image,
	                    CAST(latestPrice.{id} AS real) as CurrentPrice,                        
	                    CoinMarkets.MarketCap,
	                    CoinMarkets.MarketCapRank,
	                    CoinMarkets.FullyDilutedValuation,
	                    CoinMarkets.TotalVolume,
	                    CoinMarkets.High24h,
	                    CoinMarkets.Low24h,
	                    CoinMarkets.PriceChange24h,
	                    CoinMarkets.PriceChangePercentage24h,
	                    CoinMarkets.MarketCapChange24h,
	                    CoinMarkets.MarketCapChangePercentage24h,
	                    CoinMarkets.CirculatingSupply,
	                    CoinMarkets.TotalSupply,
	                    CoinMarkets.MaxSupply,
	                    CoinMarkets.Ath,
	                    CoinMarkets.AthChangePercentage,
	                    CoinMarkets.AthDate,
	                    CoinMarkets.Atl,
	                    CoinMarkets.AtlDate,
	                    CoinMarkets.AtlChangePercentage,
	                    CoinMarkets.LastUpdated
                    from
                      [CoinMarkets]
                      inner join
                      (select * from ( select *, row_number() over ( partition by CoinMarketsId order by Id desc ) ranking from Price) _ where ranking = 1) latestPrice
                      on [CoinMarkets].id = latestPrice.CoinMarketsId
                      where MarketCapRank != 0 order by MarketCapRank
                      offset {skip} rows FETCH NEXT {take} ROWS ONLY;";
        return _context.CoinMarkets.FromSql(FormattableStringFactory.Create(query));
    }
}
