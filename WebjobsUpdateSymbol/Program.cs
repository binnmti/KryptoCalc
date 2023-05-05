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
var coinList = await CoinGeckoUtil.GetCoinList(httpClient);

int co = 0;
//foreach (var coin in coinList)
//{
//    coin.CreateTime = DateTime.Now;
//    coin.UpdateTime = DateTime.Now;
//    sqlConnection.Insert(coin, "Id", coin.Id);

//    Console.WriteLine($"{co}/{coinList.Count}:{coin.Id}");
//    co++;
//}

int skip = 0;
int take = 500;
while (true)
{
    coinList = coinList.Skip(skip).Take(take).ToList();
    if (coinList.Count() == 0) break;

    var ids = string.Join("%2C", coinList.Select(x => x.Id));
    var response = await httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={ids}&vs_currencies=jpy");
    var content = await response.Content.ReadAsStringAsync() ?? "";
    var lines = content.Substring(1).Split(",");
    for (int i = 0; i < lines.Length; i++)
    {
        var coin = coinList.ElementAt(i + skip);
        var line = lines[i];
        var coinName = line[1..(line.IndexOf(":")-1)];
        if (coin.Id != coinName) throw new ApplicationException($"{coin.Id} != {coinName}");
        if (!decimal.TryParse(line[(line.LastIndexOf(":") + 1)..^1], out var price)) continue;

        coin.Price = price;
        coin.CreateTime = DateTime.Now; //TODO:これはNG
        coin.UpdateTime = DateTime.Now;
        sqlConnection.Update(coin);

        Console.WriteLine($"{coinName}:{coin.Price}");
    }
    skip += take;
    Thread.Sleep(1000 * 60);
}
