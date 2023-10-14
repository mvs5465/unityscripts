using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    public class BuffController : MonoBehaviour
    {
        public List<Type> buffList = new();
        private Lifeform target;

        public void SetTarget(Lifeform target)
        {
            this.target = target;
        }

        public void AddBuff(Type buffType)
        {
            GameObject buffContainer = CreateBuffContainer();
            Buff buff = (Buff)buffContainer.AddComponent(buffType);
            if (buff.IsUnique() && buffList.Contains(buffType))
            {
                UINotifications.Notify.Invoke($"Cannot equip more {buff.name}!");
                Destroy(buffContainer);
                return;
            }
            buff.SetTarget(target);
            buff.Apply();
            buffList.Add(buffType);
        }

        public void RemoveBuff(Type buffType)
        {
            Buff buff = (Buff)gameObject.GetComponentInChildren(buffType);
            if (buff) buff.Remove();
            buffList.Remove(buffType);
        }

        private GameObject CreateBuffContainer()
        {
            GameObject buffContainer = new GameObject("BuffContainer");
            buffContainer.transform.SetParent(gameObject.transform);
            buffContainer.transform.position = gameObject.transform.position;
            return buffContainer;
        }
    }
}