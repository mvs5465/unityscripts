using UnityEngine;

namespace Bunker
{
    public abstract class PickupEffectv2Data : ItemData
    {
        public abstract void Apply(GameObject target);
    }
}