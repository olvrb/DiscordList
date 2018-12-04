using System.Threading.Tasks;
using Discord.Commands;
using FallProject.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FallProject.Models {
    public class Message {
        public ulong Id            { get; set; }
        public string Content       { get; set; }
        public ulong ChannelId     { get; set; }
        public ulong GuildId       { get; set; }
        public ulong AuthorId      { get; set; }
        public string EditsAsString { get; set; }

        public static async Task Create(SocketCommandContext context) {
            using (FallprojectContext dbContext = new FallprojectContext()) {
                // Add the message to the database.
                await dbContext.Messages.AddAsync(new Message {
                                                                 Content   = context.Message.Content,
                                                                 Id        = context.Message.Id,
                                                                 ChannelId = context.Message.Channel.Id,
                                                                 GuildId   = context.Guild.Id,
                                                                 AuthorId  = context.Message.Author.Id
                                                             });
                await dbContext.SaveChangesAsync();
            }
        }


        // EntityFramework (more like, postgres' lack of array support) only support primitive types, such as string and int, therefore I can't have EditsAsString as a List, which is what I would have preferred.
        // Instead, I encode the string to base64, and separate the edits by a comma, so I can easily decode them.
        // Other libraries such as TypeORM use this method and call them simple-arrays, see https://github.com/typeorm/typeorm/issues/460#issuecomment-299813000.

        public static async Task Update(SocketCommandContext context) {
            using (FallprojectContext dbContext = new FallprojectContext()) {
                Message msg = await dbContext.Messages.SingleOrDefaultAsync(x => x.Id == context.Message.Id);
                // Encoding to base64 to guarantee there are no commas, since that's what we're splitting it by later.
                msg.EditsAsString += $"{context.Message.Content.Base64Encode()},";
                await dbContext.SaveChangesAsync();
            }
        }
    }
}