using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace FallProject.Commands {
    public class EightBall : ModuleBase<SocketCommandContext> {
        // Responses from https://en.wikipedia.org/wiki/Magic_8-Ball 
        private readonly string[] _possibleAnswers = {
                                                        "It is certain.", " It is decidedly so.", " Without a doubt.",
                                                        " Yes - definitely.", " You may rely on it.",
                                                        " As I see it, yes.", " Most likely.", " Outlook good.",
                                                        " Yes.",
                                                        " Signs point to yes.",
                                                        " Reply hazy, try again.", " Ask again later.",
                                                        " Better not tell you now.", " Cannot predict now.",
                                                        " Concentrate and ask again.", " Don't count on it.",
                                                        " My reply is no.",
                                                        " My sources say no.",
                                                        " Outlook not so good.", " Very doubtful."
                                                    };

        [Command("8ball")]
        [Alias("8", "ball", "eightball")]
        public async Task EigthBall([Remainder] string question) {
            if (!question.EndsWith("?")) {
                await ReplyAsync("That is not a question.");
                return;
            }

            // Whatever i say is a lie.
            await ReplyAsync(_possibleAnswers[new Random().Next(0, _possibleAnswers.Length)]);
        }
    }
}