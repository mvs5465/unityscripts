using Unity.VisualScripting;
using UnityEngine;

namespace Bunker
{
    public class HealingFireController : InteractableController
    {
        private Player player;

        protected override void StartCall()
        {
            player = FindObjectOfType<Player>();
        }

        protected override void Activate()
        {
            player.AddBuff(typeof(HealingFireBuff));
        }

        protected override void Deactivate()
        {
            player.RemoveBuff(typeof(HealingFireBuff));
        }

        protected override void Interact()
        {
            // do nothing
        }
    }
}