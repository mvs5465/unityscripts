using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Items/InventoryItemData")]
    public class InventoryItemData : PickupEffectData
    {
        public override void Apply(GameObject target)
        {
            target.GetComponent<Player>().inventory.AddItem(this);
        }
    }
}