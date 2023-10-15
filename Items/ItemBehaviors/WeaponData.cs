using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Weapons/WeaponData")]
    public class WeaponData : PickupEffectData
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