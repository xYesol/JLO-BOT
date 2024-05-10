using MongoDB.Bson.Serialization.Attributes;

namespace JLO_BOT
{
    [BsonKnownTypes(typeof(AssassinEnemy), typeof(BlobEnemy), typeof(CactusEnemy), typeof(SmileyBossEnemy), typeof(SunflowerEnemy))]
    public abstract class Enemy
    {
        public abstract string Name { get; }
        public abstract int Health { get; set; }
        public abstract int MaxHealth { get; }
        public abstract int AttackDamage { get; }
        public abstract bool Attacking { get; set; }


        public Enemy()
        {
        }
    }
}
