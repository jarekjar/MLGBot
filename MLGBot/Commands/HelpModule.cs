using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MLGBot.Commands
{
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command("help"), Alias("Help"), Summary("Hello world command")]
        public async Task Help()
        {
            var builder = new EmbedBuilder();
            builder.WithAuthor("MLGBot", Context.User.GetAvatarUrl());
            builder.WithTitle("MLGBot Supported Commands");
            builder.AddField("$help", "this one right here");
            builder.AddField("$join", "joins trollbot into your voice channel");
            builder.AddField("$leave", "kicks trollbot from VC");
            builder.AddField("$play title.mp3", "plays the specified sound board file");
            builder.AddField("$list", "lists out all the current soundboard files on the server");
            builder.AddField("$horn", "MLG420AIRHORN GGEZ");
            builder.AddField("$dick", "funny message");
            builder.AddField("$panda", "call panda a furry");
            builder.AddField("$party", "moves player to empty voice channel who are playing the same game.");
            builder.AddField("$party @user @user", "moves player and tagged users to empty voice channel");
            builder.WithColor(Color.Red);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }
    }
}
