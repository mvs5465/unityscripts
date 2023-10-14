using UnityEngine;

namespace Bunker
{
    public class GroundDetector : MonoBehaviour
    {
        public GroundNotificationTarget notificationTarget;
        public float colliderRadius;

        public static void Create(GameObject parent, float radius, GroundNotificationTarget notificationTarget)
        {
            Create(parent, radius, Vector3.zero, notificationTarget);
        }

        public static void Create(GameObject parent, float radius, Vector3 offset, GroundNotificationTarget notificationTarget)
        {
            GameObject groundDetectorObject = new GameObject("GroundDetector");
            groundDetectorObject.transform.position = parent.transform.position - offset;
            groundDetectorObject.transform.SetParent(parent.transform);
            GroundDetector groundDetector = groundDetectorObject.AddComponent<GroundDetector>();
            groundDetector.colliderRadius = radius;
            groundDetector.notificationTarget = notificationTarget;
        }

        private void Start()
        {
            CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
            collider.radius = colliderRadius;
            collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                notificationTarget.Notify(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {

            if (other.gameObject.CompareTag("Ground"))
            {
                notificationTarget.Notify(false);
            }
        }
    }
}