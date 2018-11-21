using System.Threading.Tasks;
using Discord.Commands;
using Discord.Rest;

namespace FallProject.Commands {
    public class Animate : ModuleBase<SocketCommandContext> {
        [Command("animate")]
        [Alias("anim", "a")]
        public async Task AnimateCommand(int timeout, string input) {
            if (input.Length > 10) {
                // Avoid getting rate-limited by Discord.
                await ReplyAsync("Message too long.");
                return;
            }

            char[]          charArr = input.ToCharArray();
            RestUserMessage message = await Context.Channel.SendMessageAsync("Animating...");
            foreach (char c in charArr) {
                // Edit message to the current char.
                await message.ModifyAsync(m => m.Content = c.ToString());
                await Task.Delay(timeout);
            }

            await message.ModifyAsync(m => m.Content = "Done :tada:.");
        }
    }
}