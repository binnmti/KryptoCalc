using System.Data.SqlClient;
using WebjobsUpdateSymbol;

int skip = 0;
int take = 500;
using var httpClient = new HttpClient();

while (true)
{
    var coins = CoinName.CoinNames.Skip(skip).Take(take);
    if (coins.Count() == 0) break;

    var ids = string.Join("%2C", coins);
    var response = await httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={ids}&vs_currencies=jpy");
    var content = await response.Content.ReadAsStringAsync() ?? "";
    skip += take;

    Thread.Sleep(1000 * 60);
}


var connectionString = "";
using var resultSqlConnection = new SqlConnection(connectionString);
//var selectCoin = resultSqlConnection.Query<Coin>("select * from Coin");
//var ids = string.Join("%2", selectCoin.Select(x => x.id));

