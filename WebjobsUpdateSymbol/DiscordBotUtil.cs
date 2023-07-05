using Discord;
using Discord.WebSocket;

namespace WebjobsUpdateSymbol;

internal static class DiscordBotUtil
{
    public static async Task SendMessageAsync(string botToken, ulong channelId, string message)
    {
        var client = new DiscordSocketClient();
        await client.LoginAsync(TokenType.Bot, botToken);
        await client.StartAsync();
        var channel = await client.GetChannelAsync(channelId) as IMessageChannel;
        var embed = new EmbedBuilder()
              .WithTitle("失敗連絡")
              .WithUrl("https://kryptocalc.scm.azurewebsites.net/azurejobs")
              .WithColor(Color.Red)
              .WithDescription(message)
              .Build();
        await channel!.SendMessageAsync(null, false, embed);
    }
}
