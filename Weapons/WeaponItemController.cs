using UnityEngine;

namespace Bunker
{
    public class WeaponItemController : MonoBehaviour
    {
        public WeaponData weaponData;
        public WeaponData Pickup()
        {
            Destroy(gameObject);
            return weaponData;
        }
    }
}