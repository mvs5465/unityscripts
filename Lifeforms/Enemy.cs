using UnityEngine;
using System.Collections;

namespace Bunker
{
    public abstract class Enemy : Lifeform
    {
        protected abstract int GetContactDamage();

        protected override void DieCall()
        {
            if (gameObject.GetComponentInParent<EnemyController>())
            {
                gameObject.GetComponentInParent<EnemyController>().NotifyDeath();
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D coll)
        {
            Player player = coll.gameObject.GetComponent<Player>();
            if (player)
            {
                player.Damage(GetContactDamage());
            }
        }
    }
}