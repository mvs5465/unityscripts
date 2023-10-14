using UnityEngine;

namespace Bunker
{
    public class ExplosionCollisionEffect : AmmoCollisionEffect
    {
        public ExplosionCollisionData explosionCollisionData;

        private void Start()
        {
            gameObject.transform.localScale = Vector3.one * explosionCollisionData.explosionRadius;
            CircleCollider2D circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
            circleCollider2D.isTrigger = true;

            if (explosionCollisionData.pseudoAnimationData)
            {
                PsuedoAnimationController.Build(gameObject, explosionCollisionData.pseudoAnimationData);

            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Lifeform lifeform = other.GetComponent<Lifeform>();
            if (lifeform)
            {
                lifeform.Damage(explosionCollisionData.explosionDamage);
            }
        }

        public static GameObject Build(Vector3 location, ExplosionCollisionData explosionCollisionData, int collisionLayer)
        {
            GameObject explosionCollisionObject = new()
            {
                name = "ExplosionCollision",
                layer = collisionLayer,
            };
            explosionCollisionObject.transform.position = location;
            ExplosionCollisionEffect explosionCollisionEffect = explosionCollisionObject.AddComponent<ExplosionCollisionEffect>();
            explosionCollisionEffect.explosionCollisionData = explosionCollisionData;
            Destroy(explosionCollisionObject, explosionCollisionData.explosionDuration);
            return explosionCollisionObject;
        }
    }
}