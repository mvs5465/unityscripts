using UnityEngine;
using System.Collections;

namespace Bunker
{
    public class ScannerEnemy : Enemy
    {
        private float timeToFlip = 0.5f;

        override protected int GetContactDamage()
        {
            return gameSettings.RedMonsterDamage;
        }

        override protected void StartCall()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            curHealth = maxHealth = gameSettings.RedMonsterHealth;
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