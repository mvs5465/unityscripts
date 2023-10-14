using UnityEngine;

namespace Bunker
{
    public class DoorController : InteractableController
    {
        public Sprite lockedSprite;
        public Sprite unlockedSprite;
        public Sprite openSprite;
        public ItemData doorKey;

        private SpriteRenderer spriteRenderer;
        private GameSettings gameSettings;
        private GameObject doorCollider;

        protected override void StartCall()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (!spriteRenderer)
            {
                spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            }

            gameSettings = FindObjectOfType<GameController>().gameSettings;


            doorCollider = new GameObject("DoorCollider");
            doorCollider.transform.position = transform.position;
            doorCollider.transform.SetParent(gameObject.transform);
            doorCollider.AddComponent<BoxCollider2D>();
            Deactivate();
        }

        protected override void Activate()
        {
            if (doorKey == null)
            {
                spriteRenderer.sprite = unlockedSprite;
            }
            else if (doorKey != null && FindObjectOfType<Player>().inventory.HasItem(doorKey))
            {
                spriteRenderer.sprite = unlockedSprite;
            }
            else if (doorKey != null && !FindObjectOfType<Player>().inventory.HasItem(doorKey))
            {
                UINotifications.Notify.Invoke("This door requires a key!");
            }
        }

        protected override void Deactivate()
        {
            doorCollider.SetActive(true);
            spriteRenderer.sprite = lockedSprite;
        }

        protected override void Interact()
        {
            if (doorKey == null)
            {
                ToggleDoor();
            }
            else if (doorKey != null && FindObjectOfType<Player>().inventory.HasItem(doorKey))
            {
                ToggleDoor();
            }
        }

        private void ToggleDoor()
        {
            doorCollider.SetActive(!doorCollider.activeSelf);
            if (spriteRenderer.sprite == openSprite)
            {
                spriteRenderer.sprite = unlockedSprite;
            }
            else
            {
                spriteRenderer.sprite = openSprite;
            }
        }


    }
}