using UnityEngine;

namespace Bunker
{
    public abstract class Lifeform : MonoBehaviour
    {
        public Rigidbody2D rb;
        protected int curHealth;
        protected int maxHealth;
        protected int shield;
        protected GameObject shieldObject;
        protected string id = "";
        protected GameSettings gameSettings;
        protected LifeformHealthbar lifeformHealthbar;

        private void Start()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;

            curHealth = maxHealth;

            shieldObject = GameUtils.AddChildObject(gameObject, "Shield");
            SpriteRenderer sr = shieldObject.AddComponent<SpriteRenderer>();
            shieldObject.transform.localScale = Vector2.one * 1.1f;
            sr.sprite = gameSettings.shieldSprite;
            sr.sortingLayerName = "Entities";
            shieldObject.SetActive(false);

            StartCall();
        }

        public void AddHealthbar()
        {
            lifeformHealthbar = LifeformHealthbar.Build(gameObject, gameSettings.uiData.healthbarSprite).GetComponent<LifeformHealthbar>();
        }

        protected virtual void StartCall() { }
        protected virtual void DieCall() { }

        private void Die()
        {
            if (lifeformHealthbar) lifeformHealthbar.transform.localScale = Vector3.zero;
            DieCall();
        }

        public void GrantShield(int amount)
        {
            shield += amount;
            Damage(0);
        }

        private int DamageShield(int amount)
        {
            if (shield <= 0) return amount;
            if (amount < 0) return amount;

            shield -= amount;
            int leftover = shield;
            if (shield < 0) shield = 0;
            if (shield > 0)
            {
                shieldObject.SetActive(true);
            }
            else
            {
                shieldObject.SetActive(false);
            }
            if (leftover < 0)
            {
                return leftover;
            }
            else
            {
                return 0;
            }
        }

        public virtual void Damage(int amount)
        {
            if (amount >= 0) amount = DamageShield(amount); // don't heal shields, but also allow an update of shields on Damage(0)
            curHealth -= amount;
            if (curHealth > maxHealth) curHealth = maxHealth;
            if (lifeformHealthbar) lifeformHealthbar.UpdateSize(curHealth, maxHealth);
            if (curHealth <= 0) Die();
            DamageCall();
        }

        protected virtual void DamageCall() { }

        public virtual void Heal(int amount)
        {
            Damage(-amount);
        }

        public virtual int GetHealth()
        {
            return curHealth;
        }

        public virtual int GetMaxHealth()
        {
            return maxHealth;
        }

        public virtual string GetId()
        {
            return id;
        }

        public virtual void SetId(string newId)
        {
            id = newId;
        }
    }
}