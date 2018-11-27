using System;
using System.Threading.Tasks;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace FallProject.Commands {
    public class ReactList : InteractiveBase<SocketCommandContext> {
        // "NextMessageAsync" has blocking calls, so the method has to be run in asynchronous mode.
        [Command("list", RunMode = RunMode.Async)]
        public async Task ReactListCommand(params string[] options) {
            bool            hasSelectedValue = false;
            int             currentIndex     = 0;
            string          sendString       = "";
            RestUserMessage listMessage      = await Context.Channel.SendMessageAsync("Please wait...");

            // Keep waiting for input until the user has selected an option (hit enter).
            while (!hasSelectedValue) {
                // Clear the string (similar to Console.Clear).
                sendString = "```diff\n";

                for (int i = 0; i < options.Length; i++) {
                    // Current selection will be marked with an arrow.
                    if (currentIndex == i) {
                        // With the `diff` tag, + will be green
                        sendString += $"+ {options[i]} <-\n";
                    }
                    else {
                        // and - will be red.
                        sendString += $"- {options[i]}\n";
                    }
                }

                sendString += "```";

                // Edit the message to the new string.
                await listMessage.ModifyAsync(m => m.Content = sendString);

                // Get input from user.
                SocketMessage msg = await NextMessageAsync();

                // Delete the command the user gave (note: this doesn't delete the msg variable, so i can still access its content).
                await msg.DeleteAsync();

                switch (msg.Content.ToLower()) {
                    case "up": {
                        currentIndex--;
                        // Stop the arrow from going under 0
                        if (currentIndex < 0) {
                            currentIndex = 0;
                        }

                        break;
                    }
                    case "down": {
                        currentIndex++;
                        // Stop the arrow from going above the amount of options.
                        if (currentIndex >= options.Length) {
                            currentIndex = options.Length - 1;
                        }

                        break;
                    }
                    case "select": {
                        hasSelectedValue = true;
                        break;
                    }
                    default: {
                        await ReplyAndDeleteAsync("Invalid command.", timeout: TimeSpan.FromMilliseconds(1000));
                        break;
                    }
                }
            }

            await listMessage.DeleteAsync();
            await ReplyAsync($"You selected option: {options[currentIndex]}");
        }
    }
}