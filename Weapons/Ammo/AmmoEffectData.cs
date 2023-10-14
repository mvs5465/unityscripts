using UnityEngine;

namespace Bunker
{
    public abstract class AmmoEffectData : ScriptableObject
    {
        public string ammoEffectName;

        public abstract GameObject Build(GameObject parent, AmmoEffectData ammoEffectData, int layer);
    }
}