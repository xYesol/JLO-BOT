using DSharpPlus.Entities;

namespace JLO_BOT
{
    public class World1 : World
    {
        public Enemy[][] allFloorEnemies = new Enemy[][]
        {
            new Enemy[] { new BlobEnemy(), new BlobEnemy()},
            new Enemy[] { new CactusEnemy(), new CactusEnemy(), new BlobEnemy(), new BlobEnemy() },
            new Enemy[] { new AssassinEnemy(), new SunflowerEnemy() },
            new Enemy[] { new SunflowerEnemy(), new SunflowerEnemy(), new SunflowerEnemy(), new AssassinEnemy() },
            new Enemy[] { new SunflowerEnemy(), new SmileyBossEnemy(), new AssassinEnemy() },
        };

        public override Enemy[][] AllFloorEnemies => allFloorEnemies;

        public override string Name => "World 1";
        public override int WorldID => 1;


        public override int Floor => 1;
        public override int MaxFloors => 5;


        public override DiscordColor Color => DiscordColor.Green;

        public World1()
        {

        }
    }
}
