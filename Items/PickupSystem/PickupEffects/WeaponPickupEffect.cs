using UnityEngine;

namespace Bunker
{
    public class WeaponPickupEffect : PickupEffect
    {
        public override void Apply()
        {
            FindObjectOfType<Player>().GrantWeapon(itemData as WeaponData);
        }
    }
}