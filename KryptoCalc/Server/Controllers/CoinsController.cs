using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KryptoCalc.Server.Data;
using KryptoCalc.Shared;
using System.Linq;

namespace KryptoCalc.Server.Controllers
{
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
            var coinMarkets = new List<CoinMarkets>
            {
                new CoinMarkets("yen", "yen", "Japan", "Japan.png", 1),
            };
            page = Math.Max(page, 1);
            var start = (page - 1) * count;
            if(count == -1)
            {
                coinMarkets.AddRange(_context.CoinMarkets.OrderBy(x => x.MarketCapRank).AsTracking());
            }
            else
            {
                coinMarkets.AddRange(_context.CoinMarkets.OrderBy(x => x.MarketCapRank).Skip(start).Take(count).AsTracking());
            }
            return coinMarkets;
        }

        [Route("/coinMarketsCount")]
        public async Task<int> CoinMarketsCount()
            => await _context.CoinMarkets.CountAsync();
    }
}