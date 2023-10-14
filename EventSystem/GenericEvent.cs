namespace Bunker
{
    public class GenericEvent : GameEvent
    {
        public string message;

        public GenericEvent(string message)
        {
            this.message = message;
        }
    }
}