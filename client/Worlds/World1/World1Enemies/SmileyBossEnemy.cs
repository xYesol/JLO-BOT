namespace JLO_BOT
{
    public class SmileyBossEnemy : Enemy
    {
        public override string Name => "Smiley Boss";
        public override int AttackDamage => 25;
        public override int MaxHealth => 150;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }


        public SmileyBossEnemy()
        {
            Health = 150;
            Attacking = false;
        }
    }
}
