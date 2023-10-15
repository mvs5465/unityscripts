using UnityEngine;
using System.Collections;
using System;

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

        protected BuffController buffController;

        private void Start()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;

            curHealth = maxHealth;

            GameObject buffControllerObject = GameUtils.AddChildObject(gameObject, "BuffController");
            buffController = buffControllerObject.AddComponent<BuffController>();
            buffController.SetTarget(this);

            shieldObject = GameUtils.AddChildObject(gameObject, "Shield");
            SpriteRenderer sr = shieldObject.AddComponent<SpriteRenderer>();
            shieldObject.transform.localScale = Vector2.one * 1.1f;
            sr.sprite = gameSettings.shieldSprite;
            sr.sortingLayerName = "Entities";
            shieldObject.SetActive(false);

            StartCall();
        }

        protected virtual void StartCall() { }
        protected virtual void DieCall() { }

        private void Die()
        {
            DieCall();
        }

        public void GrantShield(int amount)
        {
            shield += amount;
            Damage(0);
        }

        public virtual void Damage(int amount)
        {
            if (shield > 0)
            {
                shield -= amount;
                if (shield < 0)
                {
                    curHealth += shield;
                    shield = 0;
                }
            }
            else
            {
                curHealth -= amount;
            }
            if (curHealth <= 0)
            {
                Die();
            }
            if (curHealth > maxHealth)
            {
                curHealth = maxHealth;
            }
            if (shield > 0)
            {
                shieldObject.SetActive(true);
            }
            else
            {
                shieldObject.SetActive(false);
            }
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

        public virtual void AddBuff(Type buffType)
        {
            buffController.AddBuff(buffType);
        }

        public virtual void RemoveBuff(Type buffType)
        {
            buffController.RemoveBuff(buffType);
        }
    }
}