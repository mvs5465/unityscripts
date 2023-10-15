using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Buffs/HealingOrbData")]
    public class HealingOrbData : PickupEffectData
    {
        public int duration = 30;
        public int healAmount = 1;
        public int healPeriod = 5;

        public override void Apply(GameObject target)
        {
            GameObject healingOrbObject = GameUtils.AddChildObject(target, "HealingOrb");
            healingOrbObject.AddComponent<HealingOrbBuff>().healingOrbData = this;
        }

        private class HealingOrbBuff : MonoBehaviour
        {
            public HealingOrbData healingOrbData;
            private Player player;

            private void Start()
            {
                player = FindObjectOfType<Player>();
                if (healingOrbData.duration > 0)
                {
                    Destroy(gameObject, healingOrbData.duration);
                }

                InvokeRepeating("Heal", healingOrbData.healPeriod, healingOrbData.healPeriod);
            }

            private void Heal()
            {
                player.Heal(healingOrbData.healAmount);
            }
        }
    }
}