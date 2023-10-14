using UnityEngine;

namespace Bunker
{
    public class UINotificationEvent : GameEvent
    {
        public string message;
        public float duration = 0;
        public UINotificationEvent(string message)
        {
            this.message = message;
        }
        public UINotificationEvent(string message, float duration)
        {
            this.message = message;
            this.duration = duration;
        }
    }
}