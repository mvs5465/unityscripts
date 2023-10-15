using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/InventoryItemData")]
    public class InventoryItemData : PickupEffectv2Data
    {
        public override void Apply(GameObject target)
        {
            target.GetComponent<Player>().inventory.AddItem(this);
        }
    }
}