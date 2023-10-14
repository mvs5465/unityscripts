using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    public static class PickupEffectGlue
    {
        public enum EffectType
        {
            NoPickupEffect,
            WeaponPickupEffect,
            InventoryPickupEffect,
            BuffPickupEffect,
        }

        private static readonly Dictionary<EffectType, Type> effectMap = new()
        {
            {EffectType.NoPickupEffect, typeof(NoPickupEffect)},
            {EffectType.WeaponPickupEffect, typeof(WeaponPickupEffect)},
            {EffectType.InventoryPickupEffect, typeof(InventoryPickupEffect)},
            {EffectType.BuffPickupEffect, typeof(BuffPickupEffect)},
        };

        public static Type GetPickupType(EffectType effectType)
        {
            return effectMap[effectType];
        }
    }
}