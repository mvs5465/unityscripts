using Unity.VisualScripting;
using UnityEngine;

namespace Bunker
{
    public class LifeformHealthbar : MonoBehaviour
    {
        private void Start()
        {
            UpdateSize(1, 1);

        }
        public void UpdateSize(int curHealth, int maxHealth)
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            Vector3 ls = sr.transform.localScale;
            ls.x = (float)curHealth / maxHealth;
            sr.transform.localScale = ls;
        }

        public static GameObject Build(GameObject parent, Sprite sprite)
        {
            GameObject healthbarObject = GameUtils.AddChildObject(parent, "Healthbar");
            healthbarObject.AddComponent<LifeformHealthbar>();
            SpriteRenderer spriteRenderer = healthbarObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingLayerName = "Entities";
            spriteRenderer.transform.position += Vector3.up;
            return healthbarObject;
        }
    }
}