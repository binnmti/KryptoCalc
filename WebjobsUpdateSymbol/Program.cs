using KryptoCalc.Shared;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;
using WebjobsUpdateSymbol;

Console.OutputEncoding = Encoding.UTF8;

var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";
var builder = new ConfigurationBuilder().AddEnvironmentVariables();
if (isDevelopment) builder.AddUserSecrets<Program>();
var Configuration = builder.Build();
var connectionString = Configuration["ConnectionStrings:KryptoCalcServerContext"];

using var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");

using var sqlConnection = new SqlConnection(connectionString);

//var coinMarketList = await CoinGeckoUtil.GetCoinMarketListAsync(httpClient);
int co = 0;
//foreach (var coinMarket in coinMarketList)
//{
//    sqlConnection.Insert(coinMarket, "Id", coinMarket.Id);

//    Console.WriteLine($"{co++}/{coinMarketList.Count}:{coinMarket.Id}");
//}

var coinList = await CoinGeckoUtil.GetCoinListAsync(httpClient);

foreach (var coin in coinList)
{
    coin.CreateTime = DateTime.Now;
    coin.UpdateTime = DateTime.Now;
    sqlConnection.Insert(coin, "Id", coin.Id);

    Console.WriteLine($"{co++}/{coinList.Count}:{coin.Id}");
}

int skip = 0;
int take = 500;
co = 0;
while (true)
{
    Thread.Sleep(1000 * 60);
    var rangeCoinList = coinList.Skip(skip).Take(take).ToList() ?? new List<Coin>();
    if (!rangeCoinList.Any()) break;

    await CoinGeckoUtil.SetCoinListPriceAsync(httpClient, rangeCoinList);
    foreach (var coin in rangeCoinList)
    {
        coin.UpdateTime = DateTime.Now;
        sqlConnection.Update(coin);

        Console.WriteLine($"{co++}/{coinList.Count}:{coin.Id}:{coin.Price}");
    }
    skip += take;
}
