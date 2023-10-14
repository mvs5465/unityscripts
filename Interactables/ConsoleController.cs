using System.Collections;
using Bunker;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bunker
{
    public class ConsoleController : InteractableController
    {
        public AnimationData activeAnimation;
        public AnimationData doneAnimation;

        private PsuedoAnimationController animationController;
        private bool activated = false;
        private int cooldown = 5;

        protected override void StartCall()
        {
            animationController = PsuedoAnimationController.Build(gameObject, activeAnimation).GetComponent<PsuedoAnimationController>();
        }

        protected override void Activate()
        {
            if (!activated)
            {
                gameEventController.PublishEvent(new UINotificationEvent("The AI in this area has gone rogue. Use this console to deactivate them and secure the base.", 6));
            }
        }

        protected override void Deactivate() { }

        protected override void Interact()
        {
            if (!activated)
            {
                activated = true;
                animationController.SetAnimation(doneAnimation);
                gameEventController.PublishEvent(new UINotificationEvent("AI systems returned to normal. Level complete, returning to ship!"));
                InvokeRepeating("StartCountDown", 2, 1);
            }
        }

        private void StartCountDown()
        {
            if (cooldown > 0)
            {
                gameEventController.PublishEvent(new UINotificationEvent("Boarding ship in " + cooldown));
                cooldown--;
                return;
            }
            FindObjectOfType<Player>().SavePlayerState();
            SceneManager.LoadScene(1);
        }
    }
}