namespace JLO_BOT
{
    public class BlobEnemy : Enemy
    {
        public override string Name => "Blob";
        public override int AttackDamage => 6;
        public override int MaxHealth => 10;

        private int health;
        public override int Health { get => health; set => health = value; }

        private bool attacking;
        public override bool Attacking { get => attacking; set => attacking = value; }



        public BlobEnemy()
        {
            Health = 10;
            Attacking = false;
        }
    }
}
