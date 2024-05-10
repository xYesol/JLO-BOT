namespace JLO_BOT
{
    public class BlueAssassinEnemy : Enemy
    {
        public override string Name => "Blue Assassin";
        public override int AttackDamage => 115;
        public override int MaxHealth => 230;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }

        public BlueAssassinEnemy()
        {
            Health = 230;
            Attacking = false;
        }
    }
}
