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
        await channel!.SendMessageAsync(message);
    }
}
