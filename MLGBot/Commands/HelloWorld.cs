using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace MLGBot.Commands
{
    public class HelloWorld : ModuleBase<SocketCommandContext>
    {
        [Command("balls"), Alias("dick"), Summary("Hello world command")]
        public async Task Hello()
        {
            await Context.Channel.SendMessageAsync("Eat my dick and balls, this is Jared's MLG discord app.");
        }

        [Command("panda"), Alias("kip"), Summary("Hello world command")]
        public async Task Panda()
        {
            await Context.Channel.SendMessageAsync("Panda? more like super mega furry.");
        }

        [Command("embed"), Summary("embed command")]
        public async Task Embed([Remainder]string Input = "None")
        {
            var embed = new EmbedBuilder();
            embed.WithAuthor("MLGBot", Context.User.GetAvatarUrl());
            embed.WithColor(40, 200, 150);
            embed.WithFooter("The bottom of the embed", Context.Guild.Owner.GetAvatarUrl());
            embed.WithDescription("This is a **dummy** description, with a cool link.\n" + "[My Favorte website](https://www.google.com)");
            embed.AddField("User input:", Input);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
