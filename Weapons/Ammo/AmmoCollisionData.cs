using UnityEngine;

namespace Bunker
{
    public abstract class AmmoCollisionData : ScriptableObject
    {
        public string ammoCollisionName;
        public AnimationData pseudoAnimationData;

        public abstract void FireCollisionEffect(Vector3 location, int layer);
    }
}