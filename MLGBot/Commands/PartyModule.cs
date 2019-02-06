using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace MLGBot.Commands
{
    public class PartyModule : ModuleBase<SocketCommandContext>
    {
        //on command $party
        [Command("party"), Alias("Party"), Summary("Move players playing same game to same voice channel.")]
        public async Task Party()
        {
            //move user counter
            var counter = 0;

            //if user is not playing a game, return message
            if (Context.User.Activity.Type != ActivityType.Playing)
            {
                await Context.Channel.SendMessageAsync("You are not currently playing a game, or your game is not connected to discord.");
                return;
            }
            
            //set current game to command user's game
            var currentGame = Context.User.Activity.Name;

            //set current voice channel to command user's channel
            var currentUser = Context.User as SocketGuildUser;
            var currentChannel = currentUser.VoiceChannel;

            //if user is not in a voice channel, return message
            if (currentChannel == null)
            {
                await Context.Channel.SendMessageAsync("You are not currently in a voice channel, please connect to one to party up.");
                return;
            }

            //get list of all ACTIVE users in discord
            var users = Context.Guild.Users.Where(x => x.Activity != null && x.Activity.Type == ActivityType.Playing);

            //loop through all users
            foreach (var user in users)
            {
                //if game names match, move to server
                if (user.Activity.Name == currentGame && user != currentUser)
                {
                    //increase the moved user counter
                    counter++;
                    //move user to current voice channel
                    await user.ModifyAsync(x => x.Channel = currentChannel);
                }
            }

            //if no users were switched over, return message
            if (counter == 0)
            {
                await Context.Channel.SendMessageAsync($"You are all alone playing this game. Loser.");
                return;
            }

            //return success message
            await Context.Channel.SendMessageAsync($"Moved {counter} users to voice channel {currentChannel.Name}. Enjoy playing {currentGame}!");
            return;
        }
    }
}
