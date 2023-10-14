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
        protected string id = "";

        protected BuffController buffController;

        private void Start()
        {
            curHealth = maxHealth;

            GameObject buffControllerObject = new("BuffController");
            buffController = buffControllerObject.AddComponent<BuffController>();
            buffController.transform.SetParent(gameObject.transform);
            buffController.transform.position = gameObject.transform.position;
            buffController.SetTarget(this);

            StartCall();
        }

        protected virtual void StartCall() { }
        protected virtual void DieCall() { }

        private void Die()
        {
            DieCall();
        }

        public virtual void Damage(int amount)
        {
            curHealth -= amount;
            if (curHealth <= 0)
            {
                Die();
            }
            else if (curHealth > maxHealth)
            {
                curHealth = maxHealth;
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