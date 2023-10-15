using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WeaponData")]
    public class WeaponData : PickupEffectv2Data
    {
        [Header("Weapon Properties")]
        public AmmoData ammoData;
        public float cooldown = 1.0f;
        public WeaponPlayerController.FireMode fireMode = WeaponPlayerController.FireMode.NONE;

        public override void Apply(GameObject target)
        {
            target.GetComponent<Player>().GrantWeapon(this);
        }
    }
}