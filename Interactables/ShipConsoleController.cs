using System.Collections;
using Bunker;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bunker
{
    public class ShipConsoleController : InteractableController
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
                UINotifications.NotifyTimed.Invoke("Use this console to return to the planet.", 6);
            }
        }

        protected override void Deactivate() { }

        protected override void Interact()
        {
            if (!activated)
            {
                activated = true;
                animationController.SetAnimation(doneAnimation);
                UINotifications.NotifyTimed.Invoke("Landing sequence initiated!", 4);
                InvokeRepeating("StartCountDown", 2, 1);
            }
        }

        private void StartCountDown()
        {
            if (cooldown > 0)
            {
                UINotifications.Notify.Invoke("Planetside in " + cooldown);
                cooldown--;
                return;
            }
            FindObjectOfType<Player>().SavePlayerState();
            SceneManager.LoadScene(0);
        }
    }
}