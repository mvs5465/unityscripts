using UnityEngine;

namespace Bunker
{
    public class JetpackBuff : Buff
    {
        private PsuedoAnimationController animationController;
        public Rigidbody2D trackingBody;

        protected override void ApplyCall()
        {
            // FindObjectOfType<Player>().AddJumps(gameSettings.JetpackJumpBoost);
            buffData = ScriptableObject.CreateInstance<BuffData>();
            animationController = PsuedoAnimationController.Build(gameObject, gameSettings.JetpackInactive).GetComponent<PsuedoAnimationController>();
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Entities";
            sr.sortingOrder = 1;

            Player player = target as Player;
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
            AnimationData animationToPlay = gameSettings.JetpackInactive;
            if (trackingBody.velocity.y > 0)
            {
                animationToPlay = gameSettings.JetpackActive;
            }
            if (animationController.GetAnimation() != animationToPlay)
            {
                animationController.SetAnimation(animationToPlay);
            }
        }

        protected override void RemoveCall()
        {
            Player player = target as Player;
            player.AddJumps(-2);
            player.SetJumpForce(player.GetJumpForce() * 2f / 3);
        }

        public override bool IsUnique()
        {
            return true;
        }
    }
}