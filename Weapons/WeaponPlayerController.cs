using System;
using UnityEngine;

namespace Bunker
{
    public class WeaponPlayerController : MonoBehaviour
    {
        [Serializable]
        public enum FireMode
        {
            NONE,
            SINGLE,
            BURST,
            SPREAD,
        }

        private WeaponData currentWeapon;

        private bool coolingDown = false;
        private GameSettings gameSettings;

        private void FixedUpdate()
        {
            Vector3 dir = Input.mousePosition - FindObjectOfType<Camera>().WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (dir.x > transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipY = false;
            }
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void ChangeWeapon(WeaponData newWeaponData)
        {
            currentWeapon = newWeaponData;
            gameObject.GetComponent<SpriteRenderer>().sprite = newWeaponData.sprite;
        }

        public void Initialize(WeaponData startingWeaponData)
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            gameObject.layer = 6;
            currentWeapon = startingWeaponData;
            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = startingWeaponData.sprite;
            spriteRenderer.sortingLayerName = "Entities";
            spriteRenderer.sortingOrder = 2;
            spriteRenderer.flipX = true;
            spriteRenderer.transform.localPosition = new Vector3(0, 0, 0);
        }

        public void Fire()
        {
            if (!coolingDown && currentWeapon.fireMode != FireMode.NONE)
            {
                switch (currentWeapon.fireMode)
                {
                    case FireMode.BURST:
                        FireSingle();
                        Invoke("FireSingle", 0.2f);
                        Invoke("FireSingle", 0.4f);
                        break;
                    case FireMode.SPREAD:
                        FireSpread();
                        break;
                    case FireMode.SINGLE:
                        FireSingle();
                        break;
                    default:
                        break;
                }
                coolingDown = true;
                Invoke("ClearCooldown", currentWeapon.cooldown);
            }
        }

        private void ClearCooldown()
        {
            coolingDown = false;
        }

        public void FireSingle()
        {
            AmmoUtility.FireProjectile(currentWeapon.ammoData, transform.position, FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition), gameSettings.FRIENDLY_PROJECTILE_LAYER);
        }

        private void FireSpread()
        {
            float[] angles = { -10, 5, 0, 5, 10 };
            foreach (float angle in angles)
            {
                GameObject projectile = new GameObject { layer = 8 };
                projectile.AddComponent<AmmoController>().ammoData = currentWeapon.ammoData;

                SpriteRenderer spriteRenderer = projectile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = currentWeapon.ammoData.sprite;
                spriteRenderer.sortingLayerName = "Entities";
                spriteRenderer.sortingOrder = 2;
                spriteRenderer.flipX = !FindObjectOfType<Player>().facingRight;
                projectile.AddComponent<CircleCollider2D>();

                projectile.transform.position = transform.position;
                projectile.transform.localScale = new Vector3(currentWeapon.ammoData.size, currentWeapon.ammoData.size, currentWeapon.ammoData.size);

                float angleRads = angle * Mathf.Deg2Rad;
                float cosAngle = Mathf.Cos(angleRads);
                float sinAngle = Mathf.Sin(angleRads);
                float newX = transform.right.x * cosAngle - transform.right.y * sinAngle;
                float newY = transform.right.x * sinAngle + transform.right.y * cosAngle;

                Rigidbody2D projectileRigidbody = projectile.AddComponent<Rigidbody2D>();
                projectileRigidbody.AddForce(new Vector2(newX, newY) * currentWeapon.ammoData.speed, ForceMode2D.Impulse);

                Destroy(projectile, currentWeapon.ammoData.range);
            }

        }
    }
}