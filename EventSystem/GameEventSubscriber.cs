using UnityEngine;

namespace Bunker
{
    public abstract class GameEventSubscriber
    {
        public abstract void HandleEvent(GameEvent gameEvent);
    }
}