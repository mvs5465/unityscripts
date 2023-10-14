using UnityEngine;

namespace Bunker
{
    public class MedkitBuff : Buff
    {
        protected override void ApplyCall()
        {
            buffData = ScriptableObject.CreateInstance<BuffData>();
            buffData.itemName = "Medkit";
            buffData.duration = 0.1f;
            Heal();
        }

        private void Heal()
        {
            target.Heal(gameSettings.MedkitHealAmount);
        }
        
        public override bool IsUnique()
        {
            return false;
        }

        protected override void RemoveCall()
        {
            // Nothing to do
        }
    }
}