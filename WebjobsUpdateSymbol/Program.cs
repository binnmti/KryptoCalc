using Dapper;
using KryptoCalc.Shared;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using WebjobsUpdateSymbol;

Console.OutputEncoding = Encoding.UTF8;

var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) || devEnvironmentVariable.ToLower() == "development";
var builder = new ConfigurationBuilder().AddEnvironmentVariables();
if (isDevelopment) builder.AddUserSecrets<Program>();
var Configuration = builder.Build();
var connectionString = Configuration["ConnectionStrings:KryptoCalcServerContext"];
var discordBotToken = Configuration["ConnectionStrings:DiscordBotToken"] ?? "";
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

string GetErrorMessage<T>(T data, Exception ex)
{
    if(data == null) return "";

    var values = string.Concat(string.Join(",", data.GetType().GetProperties().Select(x => x.GetValue(data)?.ToString())));
    return values + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
}

async Task Run()
{
    var coinMarketList = await CoinGeckoUtil.GetCoinMarketListAsync(httpClient);
    var priceList = await CoinGeckoUtil.GetPriceListAsync(httpClient, 
        coinMarketList.Where(x => x.MarketCapRank != 0).Select(x => x.Id).ToList());

    using (var transaction = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromHours(2)))
    {
        using var sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();
        var exceptionList = new List<string>();
        int co = 1;
        foreach (var coinMarket in coinMarketList)
        {
            try
            {
                sqlConnection.InsertOrUpdate(coinMarket, coinMarket.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(GetErrorMessage(coinMarket, ex));
                if (exceptionList.Any() && exceptionList[^1] == ex.Message) throw;
                exceptionList.Add(ex.Message);
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
                Console.WriteLine(GetErrorMessage(price, ex));
                if (exceptionList.Any() && exceptionList[^1] == ex.Message) throw;
                exceptionList.Add(ex.Message);
            }
            Console.WriteLine($"Price:{co++}/{priceList.Count}:{price.CoinMarketsId}");
            Thread.Sleep(1);
        }

        sqlConnection.Query($"Truncate table {typeof(CoinMarketView).Name};");
        var coinMarketRankingList = coinMarketList.Where(x => x.MarketCapRank != 0).OrderBy(x => x.MarketCapRank).ToList();
        foreach (var coinMarketRanking in coinMarketRankingList)
        {
            var price = priceList.Single(y => y.CoinMarketsId == coinMarketRanking.Id);
            var coinMarketView = new CoinMarketView(
            coinMarketRanking.Id,
            coinMarketRanking.Symbol.ToUpper(),
            coinMarketRanking.Name,
            coinMarketRanking.Image,
            0,
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
            price.Sat);

            try
            {
                sqlConnection.Insert(coinMarketView);
            }
            catch (Exception ex)
            {
                Console.WriteLine(GetErrorMessage(coinMarketView, ex));
                if (exceptionList.Any() && exceptionList[^1] == ex.Message) throw;
                exceptionList.Add(ex.Message);
            }
            Console.WriteLine($"Price:{co++}/{coinMarketRankingList.Count}:{coinMarketRanking.Id}");
            Thread.Sleep(1);
        }

        transaction.Complete();
        if (exceptionList.Any())
        {
            throw new Exception("Exception!!");
        }
    }

    //using var client = new CosmosClient(cosmosEndpoint, cosmosKey);
    //Database database = await client.CreateDatabaseIfNotExistsAsync("KryptoCalcDB");
    //Container container = await database.CreateContainerIfNotExistsAsync("KryptoCalcContainer", "/categoryId", 400);
    //Product createdItem = await container.CreateItemAsync<Product>(
    //item: newItem,
    //partitionKey: new PartitionKey("61dba35b-4f02-45c5-b648-c6badc0cbd79")
}
