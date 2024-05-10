namespace JLO_BOT
{
    public class SunflowerEnemy : Enemy
    {
        public override string Name => "Sunflower";
        public override int AttackDamage => 13;
        public override int MaxHealth => 38;
        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }
        public SunflowerEnemy()
        {
            Health = 38;
            Attacking = false;
        }
    }
}
