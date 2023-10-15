using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Buffs/Medkit")]
    public class MedkitItemData : PickupEffectData
    {
        public int healAmount = 5;
        public override void Apply(GameObject target)
        {
            target.GetComponent<Lifeform>().Heal(healAmount);
        }
    }
}