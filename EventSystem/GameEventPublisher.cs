using UnityEngine;

namespace Bunker
{
    public abstract class GameEventPublisher
    {
        protected abstract void SendEvent(GameEvent gameEvent);
    }
}