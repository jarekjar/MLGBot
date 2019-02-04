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
    }
}
