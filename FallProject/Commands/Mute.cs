using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace FallProject.Commands {
    public class Mute : ModuleBase<SocketCommandContext> {
        public async Task MuteCommand(IGuildUser member, int duration) { }
    }
}