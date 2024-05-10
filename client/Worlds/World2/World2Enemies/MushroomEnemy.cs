namespace JLO_BOT
{
    public class MushroomEnemy : Enemy
    {
        public override string Name => "Mushroom";
        public override int AttackDamage => 40;
        public override int MaxHealth => 77;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }

        public MushroomEnemy()
        {
            Health = 77;
            Attacking = false;
        }
    }
}
