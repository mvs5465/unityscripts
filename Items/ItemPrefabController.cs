using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    public class ItemPrefabController : MonoBehaviour
    {
        public ItemData itemData;

        private void Start()
        {
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
            if (itemData as PickupEffectData)
            {
                (itemData as PickupEffectData).Apply(target);
            }
            Destroy(gameObject);
        }
    }
}