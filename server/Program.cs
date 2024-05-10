using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using JLO_BOT.server.commands;
using MongoDB.Bson.Serialization;
using System;
using System.Threading.Tasks;

namespace JLO_BOT
{
    public sealed class Program
    {
        public static DiscordClient Client { get; set; }
        static async Task Main()
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.DiscordToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);
            Client.Ready += Client_Ready;
            Client.ComponentInteractionCreated += Client_ComponentInteractionCreated;

            var slashCommandsConfig = Client.UseSlashCommands();
            slashCommandsConfig.RegisterCommands<DungeonGameSL>();

            await Client.ConnectAsync();

            await Server.InitializeDatabase();
            Printer.PrintWelcomeMessage();

            await Task.Delay(-1);
        }

        public static GameState GameState { get; set; }

        public static async Task Client_ComponentInteractionCreated(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e)
        {
            GameState = Server.GetGameStateByUserId(e.User.Id);

            switch (e.Interaction.Data.CustomId)
            {
                case "targetEnemy0":
                    await TargetEnemySequence(s, e, 0);
                    if (GameState.GameOver)
                        await GameOver(s, e);
                    break;
                case "targetEnemy1":
                    await TargetEnemySequence(s, e, 1);
                    if (GameState.GameOver)
                        await GameOver(s, e);
                    break;
                case "targetEnemy2":
                    await TargetEnemySequence(s, e, 2);
                    if (GameState.GameOver)
                        await GameOver(s, e);
                    break;
                case "targetEnemy3":
                    await TargetEnemySequence(s, e, 3);
                    if (GameState.GameOver)
                        await GameOver(s, e);
                    break;
                case "targetEnemy4":
                    await TargetEnemySequence(s, e, 4);
                    if (GameState.GameOver)
                        await GameOver(s, e);
                    break;
                case "selectInventory":
                    await DisplayInventory(s, e);
                    break;
                case "selectWorld0":
                    await DisplayHub(s, e);
                    break;
                case "selectWorld1":
                    await RunWorld(s, e, 1);
                    break;
                case "selectWorld2":
                    await RunWorld(s, e, 2);
                    break;
                case "selectInventoryReturn":
                    await DisplayHub(s, e);
                    break;
                // Add more cases as needed
                default:
                    await e.Interaction.CreateResponseAsync(
                        InteractionResponseType.ChannelMessageWithSource,
                        new DiscordInteractionResponseBuilder().WithContent($"how did you get here...\n{e.User.Mention} has pressed {e.Interaction.Data.CustomId}"));
                    break;
            }
            Server.UpdateGameState(GameState, e.Interaction.User.Id);
        }

        public static async Task TargetEnemySequence(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e, int enemyID)
        {
            PlayerAttackedEnemy(enemyID);
            GameState.AttackPhase = true;

            await DisplayGame(s, e);

            bool advanceToNextFloor = false;
            for (int i = 0; i < GameState.CurrentEnemies.Length; i++)
            {
                if (GameState.CurrentEnemies[i].Health == 0)
                {
                    advanceToNextFloor = true;
                    continue;
                }
                else
                {
                    advanceToNextFloor = false;
                    await DisplayEnemyAttacking(s, e, i);
                    await DisplayPlayerAttacked(s, e, i);
                }
            }

            GameState.AttackPhase = false;
            await UpdateGame(s, e);

            if (advanceToNextFloor)
                await AdvanceFloor(s, e);

            Server.UpdateGameState(GameState, e.Interaction.User.Id);
        }

        public static async Task DisplayInventory(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e)
        {
            var message = GameState.LoadInventory();
            await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder()
            .AddEmbed(message.Embed)
                .AddComponents(message.Components));
        }

        public static async Task DisplayHub(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e)
        {

            GameState = new GameState(0, e.Interaction.User);
            var message = GameState.LoadWorldHub();
            await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder()
            .AddEmbed(message.Embed)
                .AddComponents(message.Components));
        }
        public static async Task DisplayGame(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e)
        {
            var message = GameState.LoadWorld();
            await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder()
            .AddEmbed(message.Embed)
                .AddComponents(message.Components));
        }

        public static async Task UpdateGame(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e)
        {
            var message = GameState.LoadWorld();
            await e.Interaction.EditOriginalResponseAsync(new DiscordWebhookBuilder()
                .AddEmbed(message.Embed)
                .AddComponents(message.Components));
        }
        public static async Task RunWorld(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e, int worldNumber)
        {
            GameState = new GameState(worldNumber, e.Interaction.User);
            await DisplayGame(s, e);
            Server.UpdateGameState(GameState, e.Interaction.User.Id);

        }

        public static async Task GameOver(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e)
        {
            GameState.AttackPhase = true;
            var message = GameState.LoadWorld();
            var updatedEmbed = new DiscordEmbedBuilder(message.Embed).WithTitle("You died! git gud");
            await e.Interaction.EditOriginalResponseAsync(new DiscordWebhookBuilder()
                .AddEmbed(updatedEmbed)
                .AddComponents(message.Components));
        }

        public static async Task AdvanceFloor(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e)
        {
            await Task.Delay(500);
            var message = GameState.LoadWorld();
            var updatedEmbed = new DiscordEmbedBuilder(message.Embed).WithTitle("Advancing to next floor...");
            await e.Interaction.EditOriginalResponseAsync(new DiscordWebhookBuilder()
                .AddEmbed(updatedEmbed)
                .AddComponents(message.Components));
            await Task.Delay(1000);
            Server.UpdateWorldFloorsCleared(e.User.Id, GameState.World.WorldID, GameState.Floor);
            GameState.Floor++;
            GameState.Player.CurrentHealth = GameState.Player.MaxHealth;
            GameState.CurrentEnemies = GameState.World.CurrentEnemies(GameState.Floor);
            await UpdateGame(s, e);
        }

        public static async Task DisplayEnemyAttacking(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e, int enemyID)
        {
            await Task.Delay(250);
            GameState.CurrentEnemies[enemyID].Attacking = true;
            await UpdateGame(s, e);
        }

        public static async Task DisplayPlayerAttacked(DiscordClient s, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e, int enemyID)
        {
            await Task.Delay(250);
            GameState.CurrentEnemies[enemyID].Attacking = true;
            GameState.Player.CurrentHealth -= Math.Max(GameState.CurrentEnemies[enemyID].AttackDamage - GameState.Player.Armor, 0);
            if (GameState.Player.CurrentHealth < 0)
            {
                GameState.GameOver = true;
                GameState.Player.CurrentHealth = 0;
            }
            await UpdateGame(s, e);
            GameState.CurrentEnemies[enemyID].Attacking = false;
        }

        public static void PlayerAttackedEnemy(int enemyID)
        {
            GameState.CurrentEnemies[enemyID].Health -= GameState.Player.AttackDamage;

            if (GameState.CurrentEnemies[enemyID].Health < 0)
                GameState.CurrentEnemies[enemyID].Health = 0;
        }


        private static Task Client_Ready(DiscordClient s, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
