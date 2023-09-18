using Microsoft.Azure.Cosmos;
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
var discordBotToken = Configuration["ConnectionStrings:DiscordBotToken"];
var cosmosEndpoint = Configuration["ConnectionStrings:CosmosEndpoint"];
var cosmosKey = Configuration["ConnectionStrings:CosmosKey"];
ulong.TryParse(Configuration["ConnectionStrings:DiscordChannelId"], out var discordChannelId);
using var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");

try
{
    await Run();
}
catch(Exception ex)
{
    await DiscordBotUtil.SendMessageAsync(discordBotToken, discordChannelId, ex.Message);
    throw;
}

async Task Run()
{
    using var sqlConnection = new SqlConnection(connectionString);
    sqlConnection.Open();
    using var transaction = sqlConnection.BeginTransaction();
    var exceptionList = new List<string>();

    var coinMarketList = await CoinGeckoUtil.GetCoinMarketListAsync(httpClient);
    var priceList = await CoinGeckoUtil.GetPriceListAsync(httpClient, 
        coinMarketList.Where(x => x.MarketCapRank != 0).Select(x => x.Id).ToList());
    int co = 1;
    foreach (var coinMarket in coinMarketList)
    {
        try
        {
            sqlConnection.InsertOrUpdate(coinMarket, coinMarket.Id);
        }
        catch (Exception ex)
        {
            var values = string.Concat(string.Join(",", coinMarket.GetType().GetProperties().Select(x => x.GetValue(coinMarket).ToString())));
            var value = values + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            if (exceptionList.Any() && exceptionList[^1] == value) throw;
            exceptionList.Add(value);
        }
        Console.WriteLine($"CoinMarket:{co++}/{coinMarketList.Count}:{coinMarket.Id}");
        Thread.Sleep(1);
    }
    co = 1;
    foreach (var price in priceList)
    {
        try
        {
            sqlConnection.Insert(price);
        }
        catch (Exception ex)
        {
            var values = string.Concat(string.Join(",", price.GetType().GetProperties().Select(x => x.GetValue(price).ToString())));
            var value = values + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            if (exceptionList.Any() && exceptionList[^1] == value) throw;
            exceptionList.Add(value);
        }
        Console.WriteLine($"Price:{co++}/{priceList.Count}:{price.CoinMarketsId}");
        Thread.Sleep(1);
    }
    transaction.Commit();
    if (exceptionList.Any())
    {
        throw new Exception("Exception!!");
    }

    //using var client = new CosmosClient(cosmosEndpoint, cosmosKey);
    //Database database = await client.CreateDatabaseIfNotExistsAsync("KryptoCalcDB");
    //Container container = await database.CreateContainerIfNotExistsAsync("KryptoCalcContainer", "/categoryId", 400);
    //Product createdItem = await container.CreateItemAsync<Product>(
    //item: newItem,
    //partitionKey: new PartitionKey("61dba35b-4f02-45c5-b648-c6badc0cbd79")
}
