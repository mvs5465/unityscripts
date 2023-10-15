using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunker

{
    public class ChestController : InteractableController
    {
        public Sprite openSprite;
        public Sprite closedSprite;
        public List<GameObject> itemPrefabs;

        public static Action<bool> UpdateLockState;
        public static Action Open;

        private bool open = false;
        private bool locked = true;
        private SpriteRenderer spriteRenderer;

        protected override void StartCall()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            UpdateLockState += SetLockState;
            Open += OpenChest;
        }

        private void SetLockState(bool newLockedState)
        {
            locked = newLockedState;
            if (locked) CloseAndLock();
        }

        private void CloseAndLock()
        {
            open = false;
            locked = true;
            spriteRenderer.sprite = closedSprite;
        }

        private void SpawnItem()
        {
            if (itemPrefabs.Count == 0) return;

            int itemIndex = UnityEngine.Random.Range(0, itemPrefabs.Count - 1);
            GameObject item = Instantiate(itemPrefabs[itemIndex], transform.position, Quaternion.identity);
        }

        protected override void Interact()
        {
            if (!locked && !open)
            {
                OpenChest();
            }
        }

        private void OpenChest()
        {
            open = true;
            locked = true;
            spriteRenderer.sprite = open ? openSprite : closedSprite;
            SpawnItem();
            Invoke("CloseAndLock", 5);
        }

        protected override void Activate() { }
        protected override void Deactivate() { }
    }
}