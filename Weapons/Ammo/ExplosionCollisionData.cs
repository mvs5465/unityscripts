using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Ammo/ExplosionCollisionData")]
    public class ExplosionCollisionData : AmmoCollisionData
    {
        public int explosionDamage = 0;
        public float explosionRadius = 1;
        public float explosionDuration = 1;

        public override void FireCollisionEffect(Vector3 location, int layer)
        {
            ExplosionCollisionEffect.Build(location, this, layer);
        }
    }
}