namespace JLO_BOT
{
    public class MoonBossEnemy : Enemy
    {
        public override string Name => "Moon Boss";
        public override int AttackDamage => 185;
        public override int MaxHealth => 580;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }

        public MoonBossEnemy()
        {
            Health = 580;
            Attacking = false;
        }
    }
}
