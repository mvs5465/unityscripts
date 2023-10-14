using UnityEngine;
using System.Text;
using UnityEditor;

namespace Bunker
{
    public class Spawner : MonoBehaviour
    {
        public GameObject monsterPrefab;
        public int maxSpawns;

        private string spawnerId;

        void Start()
        {
            spawnerId = GenerateRandomId();
            InvokeRepeating("SpawnMonster", 1, 3);
        }

        private void SpawnMonster()
        {
            if (ShouldSpawnMore())
            {
                GameObject newMonster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
                newMonster.GetComponent<EnemyController>().SetId(spawnerId);
                //newMonster.GetComponent<Rigidbody2D>()?.AddForce(transform.up * Random.Range(0.5f, 2f), ForceMode2D.Impulse);
            }
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
                sb.Append(possibleLetters[Random.Range(0, possibleLetters.Length)]);
            }
            return sb.ToString();
        }
    }
}
