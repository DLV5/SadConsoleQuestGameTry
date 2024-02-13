namespace ConsoleStyleFantasy
{
    internal class PlayerStats
    {
        public PlayerHealth Health { get; set; }
        public PlayerGold Gold { get; set; }

        public PlayerUI UI { get; set; }

        public PlayerStats(int health) 
        {
            Health = new PlayerHealth(health);
            Gold = new PlayerGold();

            UI = new PlayerUI(this);
        }
    }
}
