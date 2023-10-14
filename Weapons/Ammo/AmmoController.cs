using UnityEngine;

namespace Bunker
{
    public class AmmoController : MonoBehaviour
    {
        public AmmoData ammoData;

        private void OnCollisionEnter2D(Collision2D other)
        {
            Lifeform lifeform = other.gameObject.GetComponentInChildren<Lifeform>();
            if (lifeform)
            {
                lifeform.Damage(ammoData.damage);
            }
            if (ammoData.collisionEffectData)
            {
                ammoData.collisionEffectData.FireCollisionEffect(gameObject.transform.position, gameObject.layer);
            }
            Destroy(gameObject);
        }
    }
}