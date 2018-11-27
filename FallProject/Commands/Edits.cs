using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FallProject.Models;
using FallProject.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FallProject.Commands {
    public class Edits : ModuleBase<SocketCommandContext> {
        [Command("edits")]
        public async Task EditsCommand(string messageId) {
            using (var dbContext = new fallprojectContext()) {
                List<string> edits   = new List<string>();
                string       sendStr = "";
                var          msg     = await dbContext.Message.FirstOrDefaultAsync(x => x.Id == messageId);
                if (msg == null) {
                    await ReplyAsync("Invalid message.");
                    return;
                    ;
                }

                var author = Context.Client.GetUser(Convert.ToUInt64(msg.AuthorId));
                if (author == null) return;
                Random r = new Random();
                var builder = new EmbedBuilder().WithAuthor(Context.Client.CurrentUser.Username,
                                                            Context.Client.CurrentUser.GetAvatarUrl())
                                                .WithTimestamp(DateTimeOffset.Now)
                                                .WithColor(Color.Red)
                                                .WithTitle($"Edits for message with ID {messageId} from {author.Username}")
                                                .WithDescription(String
                                                                     .Join(Environment.NewLine,
                                                                           msg.EditsAsString.Split(",")
                                                                              .Select(x =>
                                                                                          $"{Base64Utilities.Decode(x)}\n")));
                Context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }
    }
}