using UnityEngine;
using System.Collections;

namespace Bunker
{
    public class Martian : Enemy
    {
        private int moveSpeed = 1;
        private bool dying = false;
        private GameSettings gameSettings;

        override protected int GetContactDamage()
        {
            return gameSettings.MartianDamage;
        }

        override protected void StartCall()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            curHealth = maxHealth = gameSettings.MartianHealth;
            StartCoroutine("WalkAround");
        }

        override protected void DieCall()
        {
            if (!dying)
            {
                dying = true;
                GameObject.Find(gameSettings.PlayerGameObjectName).GetComponent<Player>().IncrementKillCount();
                StopCoroutine("WalkAround");
                Destroy(gameObject.GetComponent<Collider2D>());
                Destroy(gameObject, 1);
            }
        }

        private void Update() {
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0);
        }

        IEnumerator WalkAround()
        {
            while (true)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                moveSpeed = -moveSpeed;
                yield return new WaitForSeconds(Random.Range(1, 3));
            }
        }
    }
}