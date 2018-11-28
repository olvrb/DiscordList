using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using FallProject.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FallProject {
    public class Program {
        private const string              Prefix = "!";
        private       DiscordSocketClient _client;
        private       CommandService      _commands;
        private       IServiceProvider    _services;
        private       string              Token { get; set; }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args) => new Program().RunBotAsync()
                                                                .GetAwaiter()
                                                                .GetResult();

        public async Task RunBotAsync() {
            // Load the bot token from token.txt.
            // I know there are better ways to do configuration files, but this was the fastest and easiest I could think of
            // (same goes for the database connection string).
            Token     = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "token.txt")).Trim();
            _client   = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                        .AddSingleton(_client)
                        .AddSingleton(_commands)
                        .AddSingleton<InteractiveService>()
                        .BuildServiceProvider();

            _client.Log            += Log;
            _client.MessageUpdated += MessageEdit;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, Token);

            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg) {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync() {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }


        private async Task HandleCommandAsync(SocketMessage arg) {
            await StoreMessage(arg);
            SocketUserMessage message = arg as SocketUserMessage;
            if (message is null || message.Author.IsBot || message.Author.IsWebhook) {
                return;
            }


            int argPos = 0;

            // Make sure the message starts with the prefix, or mentions the CurrentUser
            if (message.HasStringPrefix(Prefix, ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) {
                SocketCommandContext context = new SocketCommandContext(_client, message);

                IResult res = await _commands.ExecuteAsync(context, argPos, _services);
                if (!res.IsSuccess) {
                    Console.WriteLine(res.ErrorReason);
                }
            }
        }

        private async Task MessageEdit(Cacheable<IMessage, ulong> before, SocketMessage message,
                                       ISocketMessageChannel      channel) {
            // We can do this inline since we won't use the context anymore.
            await Message.Update(new SocketCommandContext(_client, message as SocketUserMessage));
        }

        // Log all messages in a database for the future :).
        private async Task StoreMessage(SocketMessage message) {
            await Message.Create(new SocketCommandContext(_client, message as SocketUserMessage));
        }
    }
}