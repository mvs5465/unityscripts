namespace Bunker
{
    public class PlayerHealthEvent : GameEvent
    {
        public int curHealth;
        public int maxHealth;

        public PlayerHealthEvent (int curHealth, int maxHealth) {
            this.curHealth = curHealth;
            this.maxHealth = maxHealth;
        }
    }
}