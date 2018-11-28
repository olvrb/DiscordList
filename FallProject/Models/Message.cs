using System.Threading.Tasks;
using Discord.Commands;
using FallProject.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FallProject.Models {
    public class Message {
        public string Id            { get; set; }
        public string Content       { get; set; }
        public string ChannelId     { get; set; }
        public string GuildId       { get; set; }
        public string AuthorId      { get; set; }
        public string EditsAsString { get; set; }

        public static async Task Create(SocketCommandContext context) {
            using (FallprojectContext dbContext = new FallprojectContext()) {
                // Add the message to the database.
                await dbContext.Message.AddAsync(new Message {
                                                                 Content   = context.Message.Content,
                                                                 Id        = context.Message.Id.ToString(),
                                                                 ChannelId = context.Message.Channel.Id.ToString(),
                                                                 GuildId   = context.Guild.Id.ToString(),
                                                                 AuthorId  = context.Message.Author.Id.ToString()
                                                             });
                await dbContext.SaveChangesAsync();
            }
        }

        /*
         * EntityFramework (more like, postgres' lack of array support) only support primitive types, such as string and int, therefore I can't have EditsAsString as a List, which is what I would have preferred.
         * Instead, I encode the string to base64, and separate the edits by a comma, so I can easily decode them.
         * Other libraries such as TypeORM use this method, see https://github.com/typeorm/typeorm/issues/460#issuecomment-299813000.
         */
        public static async Task Update(SocketCommandContext context) {
            using (FallprojectContext dbContext = new FallprojectContext()) {
                Message msg = await dbContext.Message.SingleOrDefaultAsync(x => x.Id == context.Message.Id.ToString());
                // Encoding to base64 to guarantee there are no commas, since that's what we're splitting it by later.
                msg.EditsAsString += $"{context.Message.Content.Base64Encode()},";
                await dbContext.SaveChangesAsync();
            }
        }
    }
}