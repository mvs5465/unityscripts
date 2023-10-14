
using UnityEngine;

namespace Bunker
{
    public class PsuedoAnimationController : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        private AnimationData animationData;

        private int currentFrame = 0;
        private float timer = 0f;


        public static GameObject Build(GameObject parent, AnimationData animationData)
        {
            return Build(parent, animationData, "Entities");
        }

        public static GameObject Build(GameObject parent, AnimationData animationData, string sortingLayerName)
        {
            GameObject controllerObject = new("PseudoAnimationController");
            controllerObject.transform.position = parent.transform.position;
            controllerObject.transform.SetParent(parent.transform);
            PsuedoAnimationController controller = controllerObject.AddComponent<PsuedoAnimationController>();
            controller.animationData = animationData;

            if (!parent.GetComponent<SpriteRenderer>()) { parent.AddComponent<SpriteRenderer>(); }
            SpriteRenderer sr = parent.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = sortingLayerName;
            controller.spriteRenderer = sr;
            controller.SetAnimation(animationData);

            return controllerObject;
        }

        public void SetAnimation(AnimationData newAnimationData)
        {
            animationData = newAnimationData;
            currentFrame = 0;
            timer = 0f;
            if (animationData.frames.Count > 0)
            {
                spriteRenderer.sprite = animationData.frames[0];
            }
        }

        public AnimationData GetAnimation()
        {
            return animationData;
        }

        public void Update()
        {
            if (animationData.frames.Count <= 1)
            {
                return;
            }

            timer += Time.deltaTime;
            if (timer >= animationData.frameRate)
            {
                timer = 0;
                currentFrame = (currentFrame + 1) % animationData.frames.Count;
                spriteRenderer.sprite = animationData.frames[currentFrame];
            }
        }
    }
}