using Discord;
using Discord.WebSocket;
using System.Text.RegularExpressions;

DiscordSocketClient client = new(new DiscordSocketConfig
{
    GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.DirectMessages
});

if (!File.Exists("Data/token.txt"))
{
    throw new FileNotFoundException("Missing token file");
}
var token = File.ReadAllText("Data/token.txt");

client.Log += (x) => {
    Console.WriteLine(x);
    return Task.CompletedTask;
};

client.MessageReceived += async (SocketMessage arg) => {
    if (arg is not SocketUserMessage msg || string.IsNullOrWhiteSpace(msg.Content))
    {
        return;
    }
    var match = Regex.Match(msg.Content, "https:\\/\\/arxiv\\.org\\/pdf\\/(\\w+\\.\\w+)\\.pdf");
    if (match.Success)
    {
        await msg.Channel.SendMessageAsync($"https://arxiv.org/abs/{match.Groups[1].Value}");
        await msg.DeleteAsync();
    }
};

await client.LoginAsync(TokenType.Bot, token);
await client.StartAsync();

await Task.Delay(-1);