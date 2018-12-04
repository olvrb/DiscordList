using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;

namespace FallProject.Models {
    public class GuildMember {
        public ulong Id       { get; set; }
        public ulong GuildId  { get; set; }
        public ulong UnmuteAt { get; set; }
        public ulong Xp       { get; set; }

        public static async Task CreateOrUpdate(SocketGuildUser member) {
            using (FallprojectContext dbContext = new FallprojectContext()) {
                GuildMember oldMember =
                    await dbContext.GuildMembers.FirstOrDefaultAsync(x => x.Id      == member.Id &&
                                                                          x.GuildId == member.Guild.Id);
                if (oldMember == null) {
                    oldMember = new GuildMember {
                                                    Id       = member.Id,
                                                    GuildId  = member.Guild.Id,
                                                    UnmuteAt = 0,
                                                    Xp       = 0
                                                };
                    await dbContext.GuildMembers.AddAsync(oldMember);
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}