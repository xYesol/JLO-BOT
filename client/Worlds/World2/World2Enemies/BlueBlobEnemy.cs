namespace JLO_BOT
{
    public class BlueBlobEnemy : Enemy
    {
        public override string Name => "Blue Blob";
        public override int AttackDamage => 65;
        public override int MaxHealth => 125;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }

        public BlueBlobEnemy()
        {
            Health = 125;
            Attacking = false;
        }
    }
}
