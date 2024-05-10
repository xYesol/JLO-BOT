using DSharpPlus.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace JLO_BOT
{
    [BsonDiscriminator("World1")]
    public class World1 : World
    {
        [BsonIgnore]
        public Enemy[][] allFloorEnemies = new Enemy[][]
        {
            new Enemy[] { new BlobEnemy(), new BlobEnemy()},
            new Enemy[] { new CactusEnemy(), new CactusEnemy(), new BlobEnemy(), new BlobEnemy() },
            new Enemy[] { new AssassinEnemy(), new SunflowerEnemy() },
            new Enemy[] { new SunflowerEnemy(), new SunflowerEnemy(), new SunflowerEnemy(), new AssassinEnemy() },
            new Enemy[] { new SunflowerEnemy(), new SmileyBossEnemy(), new AssassinEnemy() },
        };

        public override Enemy[][] AllFloorEnemies => allFloorEnemies;

        public string name = "World 1";
        public override string Name => name;


        public int worldID = 1;
        public override int WorldID => worldID;


        public int floor = 1;
        public override int Floor => floor;


        public int maxFloors = 5;
        public override int MaxFloors => maxFloors;

        public override DiscordColor Color => DiscordColor.Green;

        public World1()
        {

        }
    }
}
