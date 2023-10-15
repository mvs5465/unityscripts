using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Buffs/JetpackData")]
    public class JetpackData : PickupEffectData
    {
        public AnimationData jetpackActive;
        public AnimationData jetpackInactive;

        public override void Apply(GameObject target)
        {
            GameObject jetpackObject = GameUtils.AddChildObject(target, "Jetpack");
            jetpackObject.AddComponent<JetpackBuff>().jetpackData = this;
        }

        private class JetpackBuff : MonoBehaviour
        {
            private PsuedoAnimationController animationController;
            public Rigidbody2D trackingBody;
            public JetpackData jetpackData;

            private void Start()
            {
                animationController = PsuedoAnimationController.Build(gameObject, jetpackData.jetpackActive).GetComponent<PsuedoAnimationController>();
                SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
                sr.sortingLayerName = "Entities";
                sr.sortingOrder = 1;

                Player player = FindObjectOfType<Player>();
                trackingBody = player.GetComponent<Rigidbody2D>();
                player.AddJumps(2);
                player.SetJumpForce(player.GetJumpForce() * 1.5f);
                if (!player.facingRight)
                {
                    sr.flipX = true;
                }
            }

            private void Update()
            {
                AnimationData animationToPlay = jetpackData.jetpackInactive;
                if (trackingBody.velocity.y > 0)
                {
                    animationToPlay = jetpackData.jetpackActive;
                }
                if (animationController.GetAnimation() != animationToPlay)
                {
                    animationController.SetAnimation(animationToPlay);
                }
            }
        }
    }
}