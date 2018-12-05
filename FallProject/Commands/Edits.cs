using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using FallProject.Models;
using FallProject.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FallProject.Commands {
    public class Edits : ModuleBase<SocketCommandContext> {
        [Command("edits")]
        public async Task EditsCommand(ulong messageId) {
            using (FallprojectContext dbContext = new FallprojectContext()) {
                Message msg = await dbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
                if (msg == null) {
                    await ReplyAsync("Invalid message.");
                    return;
                }

                SocketUser author = Context.Client.GetUser(Convert.ToUInt64(msg.AuthorId));
                // Bots may have embeds, which breaks everything.
                if (author == null || author.IsBot) {
                    await ReplyAsync("Invalid message.");
                    return;
                }

                SimpleList list = new SimpleList(SimpleList.CreateListFromBase64SimpleList(msg.EditsAsString));


                EmbedBuilder builder = new EmbedBuilder().WithAuthor(Context.Client.CurrentUser.Username,
                                                                     Context.Client.CurrentUser.GetAvatarUrl())
                                                         .WithTimestamp(DateTimeOffset.Now)
                                                         .WithColor(Color.Red)
                                                         .WithTitle($"Edits for message with ID {messageId} from {author.Username}")
                                                         .WithDescription(string.Join(Environment.NewLine,
                                                                                      list.GetList()));
                await Context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }
    }
}