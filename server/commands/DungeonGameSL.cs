using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus;
using System.Threading.Tasks;
using System.Timers;
using System.Net.NetworkInformation;
using System.Text;
using JLO_BOT;

namespace JLO_BOT.server.commands
{
    public class DungeonGameSL : ApplicationCommandModule
    {
        [SlashCommand("play", "Start playing Dungeon Simulator!")]
        public async Task MyFirstSlashCommand(InteractionContext ctx)
        {
            var discordUser = ctx.Member;
            await ctx.DeferAsync();
            Program.GameState = new GameState(0, discordUser);
            var message = Program.GameState.LoadWorldHub();
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(message.Embed).AddComponents(message.Components));

        }

    }
}
