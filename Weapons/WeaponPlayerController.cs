using UnityEngine;

namespace Bunker
{
    public class WeaponPlayerController : MonoBehaviour
    {
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
            if (startingWeaponData.pseudoAnimationData)
            {
                PsuedoAnimationController.Build(gameObject, startingWeaponData.pseudoAnimationData);
            }
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
            float spreadRange = 0.5f;
            for (int i = 0; i < 5; i++)
            {
                float randX = Random.Range(-spreadRange, spreadRange);
                float randY = Random.Range(-spreadRange, spreadRange);
                AmmoUtility.FireProjectile(currentWeapon.ammoData, transform.position, FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition) + new Vector3(randX, randY), gameSettings.FRIENDLY_PROJECTILE_LAYER);
            }
        }
    }
}