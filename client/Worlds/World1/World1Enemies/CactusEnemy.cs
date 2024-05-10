namespace JLO_BOT
{
    public class CactusEnemy : Enemy
    {
        public override string Name => "Cactus";
        public override int AttackDamage => 2;
        public override int MaxHealth => 16;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }

        public CactusEnemy()
        {
            Health = 16;
            Attacking = false;
        }
    }
}