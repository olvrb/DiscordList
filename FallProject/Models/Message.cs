using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
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

        public static async Task Create(SocketCommandContext context, fallprojectContext dbContext) {
            Message msg; /* = await dbContext.Message.SingleOrDefaultAsync(x => x.Id == context.Message.Id.ToString());
                                         //.Where(x => x.Id == context.Message.Id.ToString())
                                         //.FirstOrDefaultAsync();
            if (msg != null) {
                msg.Edits.Add(msg.Content);
                //dbContext.Entry(msg).CurrentValues.SetValues(msg);
            }
            else {*/
            msg = new Message {
                                  Content   = context.Message.Content,
                                  Id        = context.Message.Id.ToString(),
                                  ChannelId = context.Message.Channel.Id.ToString(),
                                  GuildId   = context.Guild.Id.ToString(),
                                  AuthorId  = context.Message.Author.Id.ToString()
                              };
            //}
            await dbContext.Message.AddAsync(msg);
            await dbContext.SaveChangesAsync();
        }
        /*
         * EntityFramework only support primitive types, such as string and int, therefore I can't have EditsAsString as a List, which is what I would have preferred.
         * Instead, I encode the string to base64, and separate the edits by a comma, so I can easily decode them.
         * Other libraries such as TypeORM use this method, see https://github.com/typeorm/typeorm/issues/460#issuecomment-299813000.
         */
        public static async Task Update(SocketCommandContext context, fallprojectContext dbContext) {
            Message msg = await dbContext.Message.SingleOrDefaultAsync(x => x.Id == context.Message.Id.ToString());
            msg.EditsAsString += $"{Base64Utilities.Encode(context.Message.Content)},";
            await dbContext.SaveChangesAsync();
        }
    }
}