namespace JLO_BOT
{
    public class TreantEnemy : Enemy
    {
        public override string Name => "Treant";
        public override int AttackDamage => 75;
        public override int MaxHealth => 308;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }

        public TreantEnemy()
        {
            Health = 308;
            Attacking = false;
        }
    }
}
