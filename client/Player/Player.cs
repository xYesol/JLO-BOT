using MongoDB.Bson;


namespace JLO_BOT
{
    public class Player
    {
        public ObjectId Id { get; set; }
        public ulong UserId { get; set; }
        public string Username { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int AttackDamage { get; set; }
        public int Armor { get; set; }
        public int AttackSpeed { get; set; }
        public int SkillPoints { get; set; }
        public int[] WorldFloorsCleared { get; set; }


        public Player()
        {
            MaxHealth = 25;
            CurrentHealth = 25;
            AttackDamage = 6;
            Armor = 2;
            AttackSpeed = 120;
            SkillPoints = 0;

            WorldFloorsCleared = new int[3];
            WorldFloorsCleared[0] = 0;
            WorldFloorsCleared[1] = 1;
            WorldFloorsCleared[2] = 1;
        }
        public Player(Player player)
        {
            Username = player.Username;
            MaxHealth = player.MaxHealth;
            CurrentHealth = player.MaxHealth;
            AttackDamage = player.AttackDamage;
            Armor = player.Armor;
            AttackSpeed = player.AttackSpeed;
            SkillPoints = player.SkillPoints;

            WorldFloorsCleared = new int[3];
            WorldFloorsCleared[0] = player.WorldFloorsCleared[0];
            WorldFloorsCleared[1] = player.WorldFloorsCleared[1];
            WorldFloorsCleared[2] = player.WorldFloorsCleared[2];
        }

        /*public Player(int MaxHealth, int AttackDamage, int Armor, int AttackSpeed, int SkillPoints)
        {
            MaxHealth = this.MaxHealth;
            AttackDamage = this.AttackDamage;
            Armor = this.Armor;
            AttackSpeed = this.AttackSpeed;
            SkillPoints = this.SkillPoints;
        }*/
    }
}
