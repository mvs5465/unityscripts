using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WeaponData")]
    public class WeaponData : ItemData
    {
        [Header("Weapon Properties")]
        public AmmoData ammoData;
        public float cooldown = 1.0f;
        public WeaponPlayerController.FireMode fireMode = WeaponPlayerController.FireMode.NONE;

        public override PickupEffectGlue.EffectType GetPickupEffectType()
        {
            return PickupEffectGlue.EffectType.WeaponPickupEffect;
        }
    }
}