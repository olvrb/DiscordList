using System.Threading.Tasks;
using Discord.WebSocket;

namespace FallProject.Models {
    public class GuildMember {
        public string Id       { get; set; }
        public string GuildId  { get; set; }
        public ulong  UnmuteAt { get; set; }

        public static async Task CreateOrUpdate(SocketGuildUser member) {
            using (FallprojectContext dbContext = new FallprojectContext()) {
                GuildMember dbMember = new GuildMember
                                       {Id = member.Id.ToString(), GuildId = member.Guild.Id.ToString(), UnmuteAt = 0};
                await dbContext.GuildMembers.AddAsync(dbMember);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}