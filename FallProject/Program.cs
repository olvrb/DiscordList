using System;
using System.IO;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using FallProject.Models;
using Microsoft.EntityFrameworkCore;
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
            Token     = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "token.txt")).Trim();
            _client   = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                        .AddSingleton(_client)
                        .AddSingleton(_commands)
                        .AddSingleton<InteractiveService>()
                        .BuildServiceProvider();

            _client.Log += Log;

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

        // Log all messages in a database for the future :).
        private async Task StoreMessage(SocketMessage message) {
            Message newMessage = new Message();
            newMessage.createOrUpdate(message);
            using (var db = new fallprojectContext()) {
                db.Add(newMessage);
                await db.SaveChangesAsync();
            }
        }
    }
}