using DSharpPlus.Entities;


namespace JLO_BOT
{
    public abstract class World
    {
        public abstract Enemy[][] AllFloorEnemies { get; }
        public abstract string Name { get; }
        public abstract int Floor { get; }
        public abstract int MaxFloors { get; }
        public abstract int WorldID { get; }


        public abstract DiscordColor Color { get; }
        public Enemy[] CurrentEnemies(int floor)
        {
            if (floor == 0)
                return AllFloorEnemies[0];

            return AllFloorEnemies[floor - 1];
        }

        public World()
        {

        }
    }
}
