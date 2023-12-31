using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AnimationData")]
    public class AnimationData : ScriptableObject
    {
        public float frameRate = 0.1f;
        [SerializeField] public List<Sprite> frames;
    }
}