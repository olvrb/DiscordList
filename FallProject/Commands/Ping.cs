using System.Threading.Tasks;
using Discord.Commands;
using Discord.Rest;

namespace FallProject.Commands {
    public class Ping : ModuleBase<SocketCommandContext> {
        [Command("ping")]
        public async Task PingAsync() {
            RestUserMessage message = await Context.Channel.SendMessageAsync("Ping?");
            message.ModifyAsync(msg =>
                                    msg.Content =
                                        $"Pong! Current ping is {message.CreatedAt - Context.Message.CreatedAt}");
        }
    }
}