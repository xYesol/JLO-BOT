using DSharpPlus;
using DSharpPlus.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text;

namespace JLO_BOT
{
    public class GameState
    {
        [BsonIgnore]
        public ObjectId Id { get; set; }
        public World World { get; set; }

        [BsonIgnore]
        public World[] Worlds = new World[]
        {
            new WorldHub(),
            new World1(),
            new World2(),
        };

        public int Floor { get; set; }
        public Enemy[] CurrentEnemies { get; set; }
        public Player Player { get; set; }
        public bool AttackPhase { get; set; }
        public bool GameOver { get; set; }

        public GameState(int location, DiscordUser discordUser)
        {
            // Check if the user already exists in the database
            Player player = Server.GetPlayerById(discordUser.Id);
            if (player != null)
            {
                int[] newWorldFloorsCleared = { player.WorldFloorsCleared[0], player.WorldFloorsCleared[1], player.WorldFloorsCleared[2] };
                Player = new Player
                {
                    UserId = discordUser.Id,
                    Username = discordUser.Username,
                    MaxHealth = player.MaxHealth,
                    CurrentHealth = player.MaxHealth,
                    AttackDamage = player.AttackDamage,
                    Armor = player.Armor,
                    AttackSpeed = player.AttackSpeed,
                    SkillPoints = player.SkillPoints,
                    WorldFloorsCleared = newWorldFloorsCleared
                };
            }
            else
            {
                // User does not exist, add them to the database
                int[] newWorldFloorsCleared = { 0, 1, 1 };
                Player newPlayer = new Player
                {
                    UserId = discordUser.Id,
                    Username = discordUser.Username,
                    MaxHealth = 25,
                    CurrentHealth = 25,
                    AttackDamage = 6,
                    Armor = 2,
                    AttackSpeed = 120,
                    SkillPoints = 0,
                    WorldFloorsCleared = newWorldFloorsCleared
                };
                Server.CreatePlayer(newPlayer);
                Player = new Player();
            }

            switch (location)
            {
                case 0:
                    World = Worlds[0];
                    Floor = World.Floor;
                    break;
                case 1:
                    World = Worlds[1];
                    Floor = World.Floor;
                    break;
                case 2:
                    World = Worlds[2];
                    Floor = World.Floor;
                    break;
                default:

                    break;
            }
        }


        public DiscordMessageBuilder LoadWorldHub()
        {
            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(World.Color)
                    .WithTitle("Welcome to the hub!")
                    .WithDescription(LoadHub())
                    .WithFooter(World.Name))
                .AddComponents(LoadHubButtons())
                .AddComponents(new DiscordButtonComponent(ButtonStyle.Secondary, "selectInventory", "Inventory"));


            return message;
        }

        public string LoadHub()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("```");
            sb.Append("World Name\tFloors\n\n");
            for (int i = 1; i < Worlds.Length; i++)
            {
                sb.Append(Worlds[i].Name + "\t\t" + Player.WorldFloorsCleared[i] + "/" + Worlds[i].MaxFloors + "\n");
            }
            sb.Append("\n");
            sb.Append("```");

            return sb.ToString();
        }

        public DiscordButtonComponent[] LoadHubButtons()
        {
            DiscordButtonComponent[] worldButtons = new DiscordButtonComponent[Worlds.Length - 1];
            for (int i = 1; i < Worlds.Length; i++)
            {
                worldButtons[i - 1] = new DiscordButtonComponent(ButtonStyle.Primary, $"selectWorld{i}", Worlds[i].Name);
            }

            return worldButtons;
        }

        public DiscordMessageBuilder LoadInventory()
        {
            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(World.Color)
                    .WithTitle($"Inventory of {Player.Username}")
                    .WithDescription(DisplayInventory())
                    .WithFooter("Inventory"))
                .AddComponents(LoadInventoryButtons())
                .AddComponents(
                    new DiscordButtonComponent(ButtonStyle.Secondary, "selectInventoryReturn", "Return"),
                    new DiscordButtonComponent(ButtonStyle.Secondary, $"selectGear", "Gear"));


            return message;
        }
        public string DisplayInventory()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("```");
            sb.Append($"Health:\t\t{Player.MaxHealth}\n");
            sb.Append($"Damage:\t\t{Player.AttackDamage}\n");
            sb.Append($"Armor:\t\t {Player.Armor}\n");
            sb.Append($"Atk Speed:\t {Player.AttackSpeed}\n");
            sb.Append("\n");
            sb.Append($"Skill Points:  {Player.SkillPoints}\n");
            sb.Append("\n");
            sb.Append("```");

            return sb.ToString();
        }

        public DiscordButtonComponent[] LoadInventoryButtons()
        {
            DiscordButtonComponent[] inventoryButtons = new DiscordButtonComponent[4];
            if (Player.SkillPoints > 0)
            {
                inventoryButtons[0] = new DiscordButtonComponent(ButtonStyle.Primary, $"selectSkill0", "+ Health");
                inventoryButtons[1] = new DiscordButtonComponent(ButtonStyle.Primary, $"selectSkill1", "+ Damage");
                inventoryButtons[2] = new DiscordButtonComponent(ButtonStyle.Primary, $"selectSkill2", "+ Armor");
                inventoryButtons[3] = new DiscordButtonComponent(ButtonStyle.Primary, $"selectSkill3", "+ Atk Speed");
            }
            else
            {
                inventoryButtons[0] = new DiscordButtonComponent(ButtonStyle.Primary, $"selectSkill0", "+ Health", true);
                inventoryButtons[1] = new DiscordButtonComponent(ButtonStyle.Primary, $"selectSkill1", "+ Damage", true);
                inventoryButtons[2] = new DiscordButtonComponent(ButtonStyle.Primary, $"selectSkill2", "+ Armor", true);
                inventoryButtons[3] = new DiscordButtonComponent(ButtonStyle.Primary, $"selectSkill3", "+ Atk Speed", true);
            }

            return inventoryButtons;
        }



        public DiscordMessageBuilder LoadWorld()
        {
            if(CurrentEnemies == null)
                CurrentEnemies = World.CurrentEnemies(Floor);
            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(World.Color)
                    .WithTitle("Floor " + Floor)
                    .WithDescription(LoadFloor())
                    .WithFooter(World.Name))
                .AddComponents(LoadButtons());

            if (Player.CurrentHealth == 0 || AttackPhase == false)
                message.AddComponents(new DiscordButtonComponent(ButtonStyle.Secondary, "selectWorld0", "World Hub"));
            else
                message.AddComponents(new DiscordButtonComponent(ButtonStyle.Secondary, "selectWorld0", "World Hub", true));

            return message;
        }

        public string LoadFloor()
        {
            int maxEnemyNameLength = FindMaxEnemyNameLength();
            StringBuilder sb = new StringBuilder();
            int incomingDamage = 0;
            sb.Append("```");
            for (int i = 0; i < CurrentEnemies.Length; i++)
            {
                sb.Append(CurrentEnemies[i].Name + FormatSpaces(CurrentEnemies[i].Name, maxEnemyNameLength));
                if (CurrentEnemies[i].Attacking)
                {
                    incomingDamage = CurrentEnemies[i].AttackDamage;
                    sb.Append(CurrentEnemies[i].Health + "/" + CurrentEnemies[i].MaxHealth + $" *\n");
                }
                else
                    sb.Append(CurrentEnemies[i].Health + "/" + CurrentEnemies[i].MaxHealth + "\n");
            }
            if (incomingDamage > 0)
            {
                incomingDamage -= Player.Armor;
                incomingDamage = Math.Max(incomingDamage, 0);
                sb.Append($"\t\t\t(-{incomingDamage})\n");
            }
            else
                sb.Append("\n");
            sb.Append($"Your health: {Player.CurrentHealth}/{Player.MaxHealth}");

            sb.Append("```");

            return sb.ToString();
        }

        public DiscordButtonComponent[] LoadButtons()
        {
            DiscordButtonComponent[] enemyButtons = new DiscordButtonComponent[CurrentEnemies.Length];
            for (int i = 0; i < CurrentEnemies.Length; i++)
            {
                if (AttackPhase)
                {
                    for (int j = 0; j < CurrentEnemies.Length; j++)
                    {
                        enemyButtons[j] = new DiscordButtonComponent(ButtonStyle.Primary, $"targetEnemy{j}", CurrentEnemies[j].Name, true);
                    }
                    break;
                }
                if (CurrentEnemies[i].Health == 0)
                {
                    enemyButtons[i] = new DiscordButtonComponent(ButtonStyle.Primary, $"targetEnemy{i}", CurrentEnemies[i].Name, true);
                }
                else
                    enemyButtons[i] = new DiscordButtonComponent(ButtonStyle.Primary, $"targetEnemy{i}", CurrentEnemies[i].Name);
            }

            return enemyButtons;
        }
        public int FindMaxEnemyNameLength()
        {
            int maxEnemyLength = 0;
            for (int i = 0; i < CurrentEnemies.Length; i++)
            {
                if (CurrentEnemies[i].Name.Length > maxEnemyLength)
                    maxEnemyLength = CurrentEnemies[i].Name.Length;
            }

            return maxEnemyLength;
        }
        public string FormatSpaces(string enemyName, int maxEnemyLength)
        {
            int multiplier = (maxEnemyLength - enemyName.Length) + 4;
            return new string(' ', multiplier);
        }

        public string ExpandIntToString(int number, int length)
        {
            if (number == -1)
            {
                return " ";
            }

            var str = number.ToString();

            if (length <= str.Length)
                return str.Substring(0, length);

            var result = new StringBuilder(str);

            for (var i = str.Length; i < length; i++)
            {
                result.Append(" ");
            }
            return result.ToString();
        }


    }
}
