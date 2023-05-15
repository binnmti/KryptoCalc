using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KryptoCalc.Server.Data;
using KryptoCalc.Shared;

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

        [HttpGet]
        public IEnumerable<Coin> Get()
        {
            return _context.Coin;
        }

        [Route("/home/krypto")]
        public IEnumerable<CoinMarkets> KryptoGet()
        {
            var coinMarkets = new List<CoinMarkets>
            {
                new CoinMarkets("yen","yen", "Japan", "Japan.png", 1),
            };
            coinMarkets.AddRange(_context.CoinMarkets.OrderBy(x => x.MarketCapRank).Take(50).AsNoTracking());
            return coinMarkets;
        }
    }
}
