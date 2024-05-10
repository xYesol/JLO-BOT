using DSharpPlus.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace JLO_BOT
{
    [BsonDiscriminator("World2")]
    public class World2 : World
    {
        public Enemy[][] allFloorEnemies = new Enemy[][]
        {
            new Enemy[] { new MushroomEnemy(), new MushroomEnemy()},
            new Enemy[] { new BlueBlobEnemy(), new MushroomEnemy(), new BlueBlobEnemy()},
            new Enemy[] { new MushroomEnemy(), new MushroomEnemy(), new TreantEnemy(), new BlueBlobEnemy(), new BlueBlobEnemy() },
            new Enemy[] { new BlueAssassinEnemy(), new BlueAssassinEnemy() },
            new Enemy[] { new BlueAssassinEnemy(), new ButterflyEnemy(), new ButterflyEnemy(), new BlueAssassinEnemy() },
            new Enemy[] { new ButterflyEnemy(), new MoonBossEnemy(), new ButterflyEnemy()},
        };

        public override Enemy[][] AllFloorEnemies => allFloorEnemies;

        public override string Name => "World 2";
        public override int WorldID => 2;


        public override int Floor => 1;
        public override int MaxFloors => 6;

        public override DiscordColor Color => DiscordColor.Blue;

        public World2()
        {

        }
    }
}
