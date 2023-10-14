using UnityEngine;
using System.Collections;

namespace Bunker
{
    public class Fire : MonoBehaviour
    {
        private GameSettings gameSettings;
        private bool playerPresent = false;

        void Start()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            playerPresent = false;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                playerPresent = true;
                StartCoroutine(DoHeal(player));
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player)
            {
                playerPresent = false;
            }
        }

        IEnumerator DoHeal(Player player)
        {
            while (playerPresent)
            {
                player.Heal(gameSettings.FireHealAmount);
                yield return new WaitForSeconds(2);
            }
        }
    }
}