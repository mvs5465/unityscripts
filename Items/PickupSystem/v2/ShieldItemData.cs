using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Buff/Shield")]
    public class ShieldItemData : PickupEffectv2Data
    {
        public int shieldAmount = 5;

        public override void Apply(GameObject target)
        {
            target.GetComponent<Lifeform>().GrantShield(shieldAmount);
        }
    }
}