using System;
using UnityEngine;

namespace Bunker
{
    public class BuffPickupEffect : PickupEffect
    {
        public override void Apply()
        {
            FindObjectOfType<Player>().AddBuff(BuffGlue.GetAsType((itemData as BuffData).buffType));
        }
    }
}