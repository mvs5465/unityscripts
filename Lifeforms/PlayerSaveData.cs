using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/PlayerSaveData")]
    public class PlayerSaveData : ScriptableObject
    {
        [SerializeField] public List<WeaponData> weapons;
        [SerializeField] public List<Type> buffs;
        public int currentWeaponIndex;
    }
}