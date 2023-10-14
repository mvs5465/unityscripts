using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System;

namespace Bunker
{
    public class BaseDefenseController : MonoBehaviour
    {
        public List<GameObject> basicEnemies;
        public List<GameObject> rareEnemies;
        public int maxSpawns;
        public int aggroRange;

        public static Action<int> OnScoreEvent;

        private string spawnerId;
        private int totalSpawns = 0;

        private int cooldownTriggerAmount = 10;
        private int cooldownTime = 30;
        private bool coolingDown = false;

        void Start()
        {
            spawnerId = GenerateRandomId();
            InvokeRepeating("SpawnMonster", 1, 3);
        }

        private void SpawnMonster()
        {
            if (coolingDown) return;
            int randNum = UnityEngine.Random.Range(0, 100);
            List<GameObject> spawnSource = basicEnemies;
            if (randNum < 20)
            {
                spawnSource = rareEnemies;
            }
            if (ShouldSpawnMore() && !coolingDown)
            {
                GameObject newMonster = Instantiate(spawnSource[0], transform.position, Quaternion.identity);
                newMonster.GetComponent<EnemyController>().SetId(spawnerId);
                newMonster.GetComponent<EnemyController>().aggroRangeOverride = aggroRange;
                totalSpawns++;
            }
            if (totalSpawns > 0 && totalSpawns % cooldownTriggerAmount == 0)
            {
                coolingDown = true;
                Invoke("ResetCooldown", cooldownTime);
            }
        }

        private void ResetCooldown()
        {
            coolingDown = false;
        }

        private bool ShouldSpawnMore()
        {
            EnemyController[] existingLifeforms = FindObjectsOfType<EnemyController>();
            int existingLifeformCount = 0;
            foreach (EnemyController monster in existingLifeforms)
            {
                if (monster.GetId() == spawnerId)
                {
                    existingLifeformCount++;
                }
            }
            return existingLifeformCount < maxSpawns;
        }

        private string GenerateRandomId()
        {
            string possibleLetters = "ABCDEFGHIJKLMNOPQRSTUVXYZ1234567890";
            int idLength = 10;
            StringBuilder sb = new("");
            for (int i = 0; i < idLength; i++)
            {
                sb.Append(possibleLetters[UnityEngine.Random.Range(0, possibleLetters.Length)]);
            }
            return sb.ToString();
        }
    }
}
