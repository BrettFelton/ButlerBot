using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ButlerBot.Services;
using System.Threading.Tasks;
using System.Text;

namespace ButlerBot.Modules
{
    public class MusicCommands : ModuleBase<SocketCommandContext>
    {
        private MusicService _musicService;

        public MusicCommands(MusicService musicService)
        {
            _musicService = musicService;
        }

        [Command("Help")]
        public async Task HelpCommand()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Hello ");
            sb.AppendLine("Go Fuck Yourself!!");
            sb.AppendLine("");
            sb.AppendLine("No but seriously use .p to play music");
            sb.AppendLine("If you dont know what else to do then should you really be here?");
            await ReplyAsync(sb.ToString());
        }

        [Command("Join")]
        public async Task Join()
        {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync("You need to connect to a voice channel.");
                return;
            }
            else
            {
                await _musicService.ConnectAsync(user.VoiceChannel, Context.Channel as ITextChannel);
                await ReplyAsync($"now connected to {user.VoiceChannel.Name}");
            }
        }

        [Command("Leave")]
        public async Task Leave()
        {
            var user = Context.User as SocketGuildUser;
            if (user.VoiceChannel is null)
            {
                await ReplyAsync("Please join the channel the bot is in to make it leave.");
            }
            else
            {
                await _musicService.LeaveAsync(user.VoiceChannel);
                await ReplyAsync($"Bot has now left {user.VoiceChannel.Name}");
            }
        }

        [Command("Play")]
        [Alias("p","P","play")]
        public async Task Play([Remainder]string query)
        {
             var user = Context.User as SocketGuildUser;
                if (Context.Channel != user.VoiceChannel)
                {   
                    await _musicService.ConnectAsync(user.VoiceChannel, Context.Channel as ITextChannel);
                    await ReplyAsync(await _musicService.PlayAsync(query, Context.Guild.Id));
                }
                else
                {   
                    await ReplyAsync(await _musicService.PlayAsync(query, Context.Guild.Id));
                }
        }



        [Command("Stop")]
        public async Task Stop()
            => await ReplyAsync(await _musicService.StopAsync(Context.Guild.Id));

        [Command("Skip")]
        public async Task Skip()
            => await ReplyAsync(await _musicService.SkipAsync(Context.Guild.Id));

        [Command("Volume")]
        public async Task Volume(int vol)
            => await ReplyAsync(await _musicService.SetVolumeAsync(vol, Context.Guild.Id));

        [Command("Pause")]
        public async Task Pause()
            => await ReplyAsync(await _musicService.PauseOrResumeAsync(Context.Guild.Id));

        [Command("Resume")]
        public async Task Resume()
            => await ReplyAsync(await _musicService.ResumeAsync(Context.Guild.Id));

        [Command("Queue")]
        [Alias("q")] 
        public async Task Queue()
            => await ReplyAsync(await _musicService.QueueAsync(Context.Guild.Id));
    }
}