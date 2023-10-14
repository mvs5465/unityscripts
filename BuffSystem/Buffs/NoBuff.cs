using UnityEngine;

namespace Bunker
{
    public class NoBuff : Buff
    {
        public override bool IsUnique()
        {
            return false;
        }
        
        protected override void ApplyCall() {
            Remove();
        }

        protected override void RemoveCall() { }
    }
}