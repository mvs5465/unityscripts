using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Player/PlayerSaveData")]
    public class PlayerSaveData : ScriptableObject
    {
        [SerializeField] public List<WeaponData> weapons;
        public int currentWeaponIndex;
    }
}