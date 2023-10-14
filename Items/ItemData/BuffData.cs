using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu]
    public class BuffData : ItemData
    {
        [Header("Buff Properties")]
        public BuffGlue.BuffType buffType;
        public float duration = 0;
        [TextArea] public string buffDescription = "";

        public override PickupEffectGlue.EffectType GetPickupEffectType()
        {
            return PickupEffectGlue.EffectType.BuffPickupEffect;
        }
    }
}