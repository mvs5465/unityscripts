using UnityEngine;

namespace Bunker
{
    public abstract class PickupEffectData : ItemData
    {
        public abstract void Apply(GameObject target);
    }
}