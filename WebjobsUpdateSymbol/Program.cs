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
using var sqlConnection = new SqlConnection(connectionString);
var coinList = await CoinGeckoUtil.GetCoinListAsync(httpClient);

int co = 0;
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
    var rangeCoinList = coinList.Take(skip..take).ToList() ?? new List<Coin>();
    if (!rangeCoinList.Any()) break;

    await CoinGeckoUtil.SetCoinListPriceAsync(httpClient, rangeCoinList);
    foreach (var coin in rangeCoinList)
    {
        coin.UpdateTime = DateTime.Now;
        sqlConnection.Update(coin);

        Console.WriteLine($"{co++}/{coinList.Count}:{coin.Id}:{coin.Price}");
    }
    skip += take;
    Thread.Sleep(1000 * 60);
}
