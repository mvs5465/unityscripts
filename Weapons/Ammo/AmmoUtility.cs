using UnityEngine;

namespace Bunker
{
    public static class AmmoUtility
    {
        public static GameObject FireProjectile(AmmoData ammoData, Vector3 startPosition, Vector3 targetPosition, int projectileLayer)
        {
            GameObject projectile = new GameObject
            {
                name = "ProjectileObject",
                layer = projectileLayer,
            };

            targetPosition.z = 0;
            projectile.transform.position = startPosition;
            projectile.transform.localScale = Vector3.one * ammoData.size;

            SpriteRenderer spriteRenderer = projectile.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = ammoData.sprite;
            spriteRenderer.sortingLayerName = "Entities";
            spriteRenderer.sortingOrder = 2;
            spriteRenderer.flipX = ammoData.spriteFacesRight;

            projectile.AddComponent<CircleCollider2D>();
            projectile.AddComponent<AmmoController>().ammoData = ammoData;

            if (ammoData.ammoEffectData)
            {
                ammoData.ammoEffectData.Build(projectile, ammoData.ammoEffectData, projectileLayer);
            }

            Rigidbody2D projectileRigidbody = projectile.AddComponent<Rigidbody2D>();
            projectileRigidbody.gravityScale = ammoData.gravityScale;

            Vector2 targetAngle = targetPosition - startPosition;
            projectile.transform.right = -targetAngle / targetAngle.magnitude;
            projectileRigidbody.AddForce(targetAngle / targetAngle.magnitude * ammoData.speed, ForceMode2D.Impulse);

            if (ammoData.pseudoAnimationData)
            {
                PsuedoAnimationController.Build(projectile, ammoData.pseudoAnimationData);
            }

            GameObject.Destroy(projectile, ammoData.range);
            return projectile;
        }
    }
}