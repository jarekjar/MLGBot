using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Discord.WebSocket;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MLGBot.Mod
{
    public class Admin : ModuleBase<SocketCommandContext>
    {
        

        [Command("status"), Summary("Get the status for the bot.")]
        public async Task SetStatus([Remainder]string NewStatus = "None")
        {
            //grab config
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("config.json");

            var config = builder.Build();

            if (!(Context.User.Id == 185972916213514240))
            {
                await Context.Channel.SendMessageAsync(":x: You are not a moderator for MLG Bot!");
                return;
            }

            config["STATUS"] = NewStatus;
            await Context.Client.SetGameAsync(NewStatus, "", ActivityType.Watching);
        }
    }
}
