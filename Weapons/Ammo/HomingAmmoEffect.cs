
using UnityEngine;
using static System.Math;

namespace Bunker
{
    public class HomingAmmoEffect : AmmoEffect
    {
        public HomingAmmoData homingAmmoData;
        private Rigidbody2D parentBody;
        private Lifeform closestTarget;

        private bool trackingActive = false;
        private float elapsedTime = 0;

        private void Start()
        {
            parentBody = GetComponentInParent<Rigidbody2D>();
            CircleCollider2D circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
            circleCollider2D.isTrigger = true;
            circleCollider2D.radius = homingAmmoData.homingRadius;
        }

        private void FixedUpdate()
        {
            if (!trackingActive)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > homingAmmoData.homingDelay)
                {
                    trackingActive = true;
                }
                return;
            }
            if (closestTarget == null) return;
            float startingVelocity = parentBody.velocity.magnitude;
            Vector2 directionToTarget = closestTarget.transform.position - transform.position;
            Vector2 adjustedDirectionToTarget = directionToTarget * startingVelocity / directionToTarget.magnitude;

            if ((adjustedDirectionToTarget - parentBody.velocity).magnitude > 0.01)
            {
                gameObject.transform.parent.transform.right = new Vector2(adjustedDirectionToTarget.x, adjustedDirectionToTarget.y) * -1;
                parentBody.velocity = adjustedDirectionToTarget;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            Lifeform lifeform = other.GetComponent<Lifeform>();
            if (lifeform)
            {
                if (closestTarget == null)
                {
                    closestTarget = lifeform;
                    return;
                }

                if (lifeform == closestTarget)
                {
                    return;
                }

                if (Vector3.Distance(transform.position, lifeform.transform.position) < Vector3.Distance(transform.position, closestTarget.transform.position))
                {
                    closestTarget = lifeform;
                }
            }
        }
    }
}