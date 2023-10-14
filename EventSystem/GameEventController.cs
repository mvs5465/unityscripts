using System.Collections.Generic;

namespace Bunker
{
    public class GameEventController
    {
        private List<GameEventSubscriber> subscribers = new();

        public void Subscribe(GameEventSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void PublishEvent(GameEvent gameEvent)
        {
            foreach (GameEventSubscriber subscriber in subscribers)
            {
                subscriber.HandleEvent(gameEvent);
            }
        }
    }
}