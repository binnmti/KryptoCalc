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
    public IEnumerable<CoinMarketView> CoinMarkets(int page, int count)
    {
        var skip = (Math.Max(page, 1) - 1) * count;
        var take = count == -1 ? int.MaxValue : count;
        return GetCoinMarkets(skip, take);
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

    private List<CoinMarketView> GetCoinMarkets(int skip, int take)
    {
        var coinMarkets = _context.CoinMarkets.Where(x => x.MarketCapRank != 0).OrderBy(x => x.MarketCapRank).Skip(skip).Take(take).ToList();
        var query = @$"select * from ( select *, row_number() over ( partition by CoinMarketsId order by Id desc ) ranking from Price) _ where ranking = 1";
        var prices = _context.Price.FromSql(FormattableStringFactory.Create(query)).ToList();
        //var prices = _context.Price.Where(x => x.CreateTime == DateTime.Now);

        var coinMarketView = new List<CoinMarketView>();
        foreach (var coinMarket in coinMarkets)
        {
            var price = prices.Single(y => y.CoinMarketsId == coinMarket.Id);
            coinMarketView.Add(new CoinMarketView(
            coinMarket.Id,
            coinMarket.Symbol.ToUpper(),
            coinMarket.Name,
            coinMarket.Image,
            //TODO:これで良いのか？
            price.Jpy == 0 ? 1 : price.Jpy,

            0,
            false,
            price.Usd,
            price.Aed,
            price.Ars,
            price.Aud,
            price.Bdt,
            price.Bhd,
            price.Bmd,
            price.Brl,
            price.Cad,
            price.Chf,
            price.Clp,
            price.Cny,
            price.Czk,
            price.Dkk,
            price.Eur,
            price.Gbp,
            price.Hkd,
            price.Huf,
            price.Idr,
            price.Ils,
            price.Inr,
            price.Jpy,
            price.Krw,
            price.Kwd,
            price.Lkr,
            price.Mmk,
            price.Mxn,
            price.Myr,
            price.Ngn,
            price.Nok,
            price.Nzd,
            price.Php,
            price.Pkr,
            price.Pln,
            price.Rub,
            price.Sar,
            price.Sek,
            price.Sgd,
            price.Thb,
            price.Try,
            price.Twd,
            price.Uah,
            price.Vef,
            price.Vnd,
            price.Zar,
            price.Xdr,
            price.Xag,
            price.Xau,
            price.Bits,
            price.Sat));
        }
        return coinMarketView;
    }
}
