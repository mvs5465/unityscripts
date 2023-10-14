using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu]
    public class InventoryItemData : ItemData
    {
        public override PickupEffectGlue.EffectType GetPickupEffectType()
        {
            return PickupEffectGlue.EffectType.InventoryPickupEffect;
        }
    }


}