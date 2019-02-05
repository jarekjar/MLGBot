using System;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MLGBot.Services;

namespace MLGBot
{
    public class Program
    {
        private DiscordSocketClient Client;
        private CommandService Commands;
        private IConfigurationRoot config;
        private ServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            try
            {
                new Program().MainAsync().GetAwaiter().GetResult();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                Console.ReadLine();
            }
        }

        private async Task MainAsync()
        {

            serviceProvider = new ServiceCollection()
                .AddSingleton<AudioService>()
                .BuildServiceProvider();

            //grab config
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("config.json");

            config = builder.Build();

            //discord client
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug

            });

            //help commands
            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            Client.MessageReceived += Client_MessageReceived;

            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: serviceProvider);

            Client.Ready += Client_Ready;

            Client.Log += Client_Log;

            string Token = config["SECRET"];

            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync(config["STATUS"], "", ActivityType.Watching);
        }

        private async Task Client_Log(LogMessage msg)
        {
            Console.WriteLine($"{DateTime.Now} at {msg.Source} {msg.Message}");
        }
        
        private async Task Client_MessageReceived(SocketMessage msg)
        {
            var Message = msg as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);
            
            if (Context.Message == null || Context.Message.Content == "") return;

            if (Context.User.IsBot) return;

            var pos = 0;

            if (!(Message.HasCharPrefix('$', ref pos) || Message.HasMentionPrefix(Client.CurrentUser, ref pos))) return;

            var Result = await Commands.ExecuteAsync(Context, pos, services: serviceProvider);

            if (!Result.IsSuccess)
                Console.WriteLine($"{DateTime.Now} at Commands Something went wrong with executing a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
        }

    }
}
