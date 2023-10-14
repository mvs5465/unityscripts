
using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    public class EnemyController : MonoBehaviour
    {
        public EnemyData enemyData;
        [SerializeField] public List<GameObject> guaranteedDrops;

        public int aggroRangeOverride;

        private Rigidbody2D rb;
        private string spawnerId;

        private int seekingGround = 1;
        private int hoverHeight = 5;
        private bool dying = false;
        private bool facingRight = false;
        private GameSettings gameSettings;
        private Player player;

        private void Start()
        {
            Setup();
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            player = FindObjectOfType<Player>();
            InvokeRepeating("SeekGround", 1, 2);
            InvokeRepeating("Patrol", 0, 2);
            gameObject.layer = 7;
        }

        private void FixedUpdate()
        {
            if ((rb.velocity.x < 0 && !facingRight) || (rb.velocity.x > 0 && facingRight))
            {
                Vector3 scalar = transform.localScale;
                scalar.x *= -1;
                transform.localScale = scalar;
                facingRight = !facingRight;
            }
        }

        public void NotifyDeath()
        {
            if (!dying)
            {
                foreach (GameObject guaranteedDrop in guaranteedDrops)
                {
                    Instantiate(guaranteedDrop, transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)), Quaternion.identity);
                }
                dying = true;
                rb.gravityScale = 1.5f;
                gameObject.GetComponentInChildren<Lifeform>().gameObject.GetComponent<Collider2D>().enabled = false;
                CancelInvoke();
                BaseDefenseController.OnScoreEvent(1);
                Destroy(gameObject, 0.7f);
            }
        }

        public void NotifyGround(bool detected)
        {
            seekingGround = detected ? -1 : 1;
        }

        private void Setup()
        {
            // gameObject.AddComponent<SpriteRenderer>().sprite = enemyData.sprite;

            GameObject lifeformObject = new GameObject
            {
                name = "LifeformObject",
                layer = 7,
            };
            lifeformObject.AddComponent<ScannerEnemy>();

            lifeformObject.transform.position = gameObject.transform.position;
            lifeformObject.transform.SetParent(gameObject.transform);
            lifeformObject.transform.localScale = Vector3.one * enemyData.enemySize;
            lifeformObject.AddComponent<CircleCollider2D>();

            CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
            // collider.contactCaptureLayers = LayerMask.NameToLayer("Player");
            if (aggroRangeOverride > 0)
            {
                collider.radius = aggroRangeOverride;
            }
            else
            {
                collider.radius = enemyData.detectionRange;
            }

            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = enemyData.gravityScale;
            rb.drag = 0.9f;
            rb.freezeRotation = true;
            lifeformObject.GetComponent<Lifeform>().rb = rb;

            // GroundDetector.Create(gameObject, 3f, new GroundNotificationTarget(this));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                InvokeRepeating("ShootAtPlayer", 1, enemyData.cooldown);
                InvokeRepeating("FollowPlayer", 1, 1);
                CancelInvoke("Patrol");
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Ground" && Vector3.Distance(other.transform.position, transform.position) < hoverHeight)
            {
                rb.AddForce(Vector2.up);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                InvokeRepeating("Patrol", 3, 3);
                CancelInvoke("ShootAtPlayer");
                CancelInvoke("FollowPlayer");
            }
        }

        private void FollowPlayer()
        {
            Vector3 towardsPlayer = player.transform.position - transform.position;
            towardsPlayer = towardsPlayer / towardsPlayer.magnitude;
            rb.AddForce(towardsPlayer, ForceMode2D.Impulse);
        }

        private void SeekGround()
        {
            rb.AddForce(Vector3.down * seekingGround, ForceMode2D.Impulse);
        }

        private void Patrol()
        {
            float patrolDistance = 1f;
            Vector3 randDir = new Vector3(Random.Range(-patrolDistance, patrolDistance), Random.Range(-patrolDistance, patrolDistance), 0);
            rb.AddForce(randDir, ForceMode2D.Impulse);
        }

        private void ShootAtPlayer()
        {
            if (Vector3.Distance(player.transform.position, transform.position) < enemyData.weaponRange)
            {
                AmmoUtility.FireProjectile(enemyData.ammoData, transform.position, player.gameObject.transform.position, gameSettings.ENEMY_PROJECTILE_LAYER);
            }
        }

        internal void SetId(string spawnerId)
        {
            this.spawnerId = spawnerId;
        }

        public string GetId()
        {
            return spawnerId;
        }
    }
}