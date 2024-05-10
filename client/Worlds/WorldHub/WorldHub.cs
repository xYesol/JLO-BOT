using DSharpPlus.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace JLO_BOT
{
    [BsonDiscriminator("WorldHub")]

    public class WorldHub : World
    {
        public Enemy[][] allFloorEnemies = new Enemy[0][];

        public override Enemy[][] AllFloorEnemies => allFloorEnemies;

        public override string Name => "World Hub";
        public override int WorldID => 0;


        public override int Floor => 1;
        public override int MaxFloors => 1;
        public override DiscordColor Color => DiscordColor.Gray;

        public WorldHub()
        {

        }
    }
}
