using UnityEngine;

namespace Bunker
{
    public abstract class ItemData : ScriptableObject
    {
        [Header("General")]
        public string itemName;
        [TextArea] public string description;
        public Sprite sprite;
        public AnimationData pseudoAnimationData;

        [Header("World Properties")]
        public float spawnDensity = 0f;
        public float gravityScale = 1f;
    }
}