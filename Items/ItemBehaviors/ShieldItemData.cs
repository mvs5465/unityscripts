using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Buffs/Shield")]
    public class ShieldItemData : PickupEffectData
    {
        public int shieldAmount = 5;

        public override void Apply(GameObject target)
        {
            target.GetComponent<Lifeform>().GrantShield(shieldAmount);
        }
    }
}