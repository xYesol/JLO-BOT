using MongoDB.Bson.Serialization.Attributes;

namespace JLO_BOT
{
    [BsonDiscriminator("AssassinEnemy")]

    public class AssassinEnemy : Enemy
    {
        public override string Name => "Assassin";
        public override int AttackDamage => 15;
        public override int MaxHealth => 30;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }

        public AssassinEnemy()
        {
            Health = 30;
            Attacking = false;
        }

    }
}
