using UnityEngine;

namespace Bunker
{
    public class InventoryPickupEffect : PickupEffect
    {
        public override void Apply()
        {
            FindObjectOfType<Player>().inventory.AddItem(itemData);
        }
    }
}