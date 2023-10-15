using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Buff/Medkit")]
    public class MedkitItemData : PickupEffectv2Data
    {
        public int healAmount = 5;
        public override void Apply(GameObject target)
        {
            target.GetComponent<Lifeform>().Heal(healAmount);
        }
    }
}