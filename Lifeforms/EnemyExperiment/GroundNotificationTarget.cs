using System;
using UnityEngine;

namespace Bunker
{

    public class GroundNotificationTarget
    {
        private EnemyController enemyTarget;
        private Player playerTarget;

        public GroundNotificationTarget(EnemyController enemyTarget)
        {
            this.enemyTarget = enemyTarget;
        }

        public GroundNotificationTarget(Player playerTarget)
        {
            this.playerTarget = playerTarget;
        }

        public void Notify(bool value)
        {
            if (enemyTarget)
            {
                enemyTarget.NotifyGround(value);
            }
            else if (playerTarget)
            {
                playerTarget.NotifyGround(value);
            }
        }
    }
}