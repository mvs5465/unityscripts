using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Ammo/AmmoData")]
    public class AmmoData : ItemData
    {
        [Header("Ammo Properties")]
        public int damage = 1;
        public int range = 1;
        public int speed = 1;
        public float size = 1f;
        public bool spriteFacesRight = true;
        public AmmoEffectData ammoEffectData;
        public AmmoCollisionData collisionEffectData;
    }
}