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

var coinMarketList = await CoinGeckoUtil.GetCoinMarketListAsync(httpClient);
var priceList = await CoinGeckoUtil.GetPriceListAsync(httpClient, coinMarketList.Select(x => x.Id).ToList());
int co = 0;
foreach (var coinMarket in coinMarketList)
{
    try
    {
        sqlConnection.InsertOrUpdate(coinMarket, coinMarket.Id);
    }
    catch
    {
        Console.WriteLine($"coinMarket.AthDate = {coinMarket.AthDate}"); 
        throw;
    }
    Console.WriteLine($"{co++}/{coinMarketList.Count}:{coinMarket.Id}");
    Thread.Sleep(1);
}
foreach (var price in priceList)
{
    sqlConnection.Insert(price, "Id", price.Id);

    Console.WriteLine($"{co++}/{priceList.Count}:{price.Id}");
    Thread.Sleep(1);
}
