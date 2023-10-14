using System;
using System.Collections.Generic;

namespace Bunker
{
    public static class BuffGlue
    {
        public enum BuffType
        {
            NoBuff,
            HealingFireBuff,
            HealingOrbBuff,
            JetpackBuff,
            MedkitBuff,
        }

        private static readonly Dictionary<BuffType, Type> buffMap = new()
        {
            {BuffType.NoBuff, typeof(NoBuff)},
            {BuffType.HealingFireBuff, typeof(HealingFireBuff)},
            {BuffType.HealingOrbBuff, typeof(HealingOrbBuff)},
            {BuffType.JetpackBuff, typeof(JetpackBuff)},
            {BuffType.MedkitBuff, typeof(MedkitBuff)},
        };

        public static Type GetAsType(BuffType buffType) => buffMap[buffType];
    }
}