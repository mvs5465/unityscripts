using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    public class ItemPrefabController : MonoBehaviour
    {
        public ItemData itemData;
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
        }

        private void Enable()
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        public void Pickup(GameObject target)
        {
            UINotifications.Notify.Invoke("Picked up " + itemData.name);
            if (itemData as PickupEffectv2Data)
            {
                (itemData as PickupEffectv2Data).Apply(target);
            }
            Destroy(gameObject);
        }
    }
}