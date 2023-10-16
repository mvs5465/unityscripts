using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Buffs/VallE")]
    public class VallEData : PickupEffectData
    {
        public float followDistance = 2;
        public float aggroRadius = 7;
        public float cooldown = 1;
        public AmmoData projectile;
        public float healCooldown = 2;
        public AmmoData healingProjectile;

        public override void Apply(GameObject target)
        {
            GameObject vallEObject = new("VallE") { layer = LayerMask.NameToLayer("Allies") };
            vallEObject.transform.position = target.transform.position + Vector3.up * 2;

            SpriteRenderer sr = vallEObject.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;
            sr.sortingLayerName = "Entities";
            if (pseudoAnimationData) PsuedoAnimationController.Build(vallEObject, pseudoAnimationData);

            Rigidbody2D rb = vallEObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = 0.8f;

            VallE vallE = vallEObject.AddComponent<VallE>();
            vallE.vallEData = this;
            vallE.followTarget = target;
        }

        private class VallE : MonoBehaviour
        {
            public VallEData vallEData;
            public GameObject followTarget;

            private Rigidbody2D rb;
            private float shootTimer = 0f;
            public GameObject closestTarget;
            private float healTimer = 0f;

            private void Start()
            {
                rb = gameObject.GetComponent<Rigidbody2D>();
                CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
                collider.radius = vallEData.aggroRadius;
                collider.isTrigger = true;
            }

            private void OnTriggerStay2D(Collider2D other)
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy)
                {
                    if (closestTarget == null)
                    {
                        closestTarget = other.gameObject;
                        return;
                    }

                    if (other.gameObject == closestTarget)
                    {
                        return;
                    }

                    if (Vector3.Distance(transform.position, other.transform.position) < Vector3.Distance(transform.position, closestTarget.transform.position))
                    {
                        closestTarget = other.gameObject;
                    }
                }
            }

            private void FixedUpdate()
            {
                Vector3 targetPoint = followTarget.transform.position + Vector3.up;
                if (Vector3.Distance(transform.position, targetPoint) > vallEData.followDistance)
                {
                    Vector3 towardsTarget = targetPoint - transform.position;
                    rb.velocity = towardsTarget;
                }

                if (closestTarget != null)
                {
                    shootTimer += Time.deltaTime;
                    if (shootTimer >= vallEData.cooldown)
                    {
                        shootTimer = 0;
                        AmmoUtility.FireProjectile(vallEData.projectile, transform.position, closestTarget.transform.position, gameObject.layer);
                    }
                }

                if (followTarget.GetComponent<Player>().GetHealth() < followTarget.GetComponent<Player>().GetMaxHealth())
                {
                    healTimer += Time.deltaTime;
                    if (healTimer >= vallEData.healCooldown)
                    {
                        healTimer = 0;
                        AmmoUtility.FireProjectile(vallEData.healingProjectile, transform.position, followTarget.transform.position, LayerMask.NameToLayer("EnemyProjectiles"));
                    }
                }
            }
        }
    }
}