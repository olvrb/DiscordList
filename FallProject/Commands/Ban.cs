using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace FallProject.Commands {
    public class Ban : ModuleBase<SocketCommandContext> {
        [Command("ban")]
        // check for necessary permissions
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        // make sure the message was sent in a guild.
        [RequireContext(ContextType.Guild)]
        public async Task BanCommand(IGuildUser user, [Remainder] string reason = "") {
            //await Context.Guild.AddBanAsync(user: user, reason: reason);
            await ReplyAsync($"Banned {user}.");
            await Context.Message.DeleteAsync();
        }
    }
}