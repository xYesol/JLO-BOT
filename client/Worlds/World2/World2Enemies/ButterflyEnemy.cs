namespace JLO_BOT
{
    public class ButterflyEnemy : Enemy
    {
        public override string Name => "Butterfly";
        public override int AttackDamage => 120;
        public override int MaxHealth => 182;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }

        public ButterflyEnemy()
        {
            Health = 182;
            Attacking = false;
        }
    }
}
