using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    public class ItemPrefabController : MonoBehaviour
    {
        public ItemData itemData;

        private PickupEffect pickupEffect;
        private GameSettings gameSettings;

        private void Start()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            if (itemData.sprite)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = itemData.sprite;
            }

            if (itemData.pseudoAnimationData)
            {
                PsuedoAnimationController.Build(gameObject, itemData.pseudoAnimationData);
            }

            gameObject.AddComponent<CircleCollider2D>();
            gameObject.AddComponent<Rigidbody2D>().gravityScale = itemData.gravityScale;

            GameObject pickupEffectContainer = CreatePickupEffectContainer();
            pickupEffect = (PickupEffect)pickupEffectContainer.AddComponent(PickupEffectGlue.GetPickupType(itemData.GetPickupEffectType()));
            pickupEffect.SetItemData(itemData);
        }

        public void Pickup()
        {
            UINotifications.Notify.Invoke("Picked up " + itemData.name);
            pickupEffect.Apply();
            Destroy(gameObject);
        }

        private GameObject CreatePickupEffectContainer()
        {
            GameObject buffContainer = new("PickupEffectContainer");
            buffContainer.transform.SetParent(gameObject.transform);
            return buffContainer;
        }
    }
}