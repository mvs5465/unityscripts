using UnityEngine;
using System.Collections;

namespace Bunker
{
    public class RedMonster : Enemy
    {
        private float timeToFlip = 0.5f;
        private bool dying = false;
        private GameSettings gameSettings;

        override protected int GetContactDamage()
        {
            return gameSettings.RedMonsterDamage;
        }

        override protected void StartCall()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            curHealth = maxHealth = gameSettings.RedMonsterHealth;
            rb.gravityScale = 0;

            dying = false;

            Destroy(gameObject, 15); // prevent overpopulation

            StartCoroutine("ChangeSpin");
        }

        override protected void DieCall()
        {
            if (!dying)
            {
                dying = true;
                rb.gravityScale = 1;
                GameObject.Find(gameSettings.PlayerGameObjectName).GetComponent<Player>().IncrementKillCount();
                StopCoroutine("ChangeSpin");
                Destroy(gameObject, 1);
            }
        }

        IEnumerator ChangeSpin()
        {
            while (true)
            {
                var randNum = Random.Range(-1f, 1f);
                if (randNum < 0)
                {
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
                rb.AddForce(transform.right * randNum, ForceMode2D.Impulse);
                yield return new WaitForSeconds(timeToFlip);
            }
        }
    }
}