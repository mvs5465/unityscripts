using UnityEngine;

namespace Bunker
{
    public class HealingFireBuff : Buff
    {
        public static readonly int HEAL_PERIOD = 3;

        protected override void ApplyCall()
        {
            buffData = ScriptableObject.CreateInstance<BuffData>();
            InvokeRepeating("Heal", 5, 5);
        }

        protected override void RemoveCall()
        {
            CancelInvoke("Heal");
        }

        private void Heal()
        {
            target.Heal(gameSettings.FireHealAmount);
        }

        
        public override bool IsUnique()
        {
            return true;
        }
    }
}