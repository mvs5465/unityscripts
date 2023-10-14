using UnityEngine;

namespace Bunker
{

    [CreateAssetMenu]
    public class EnemyData : ScriptableObject
    {
        public float enemySize = 1;
        public Sprite sprite;
        public float detectionRange;
        public int weaponRange;
        public AmmoData ammoData;
        public float cooldown;
        public float gravityScale;
    }
}