using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunker
{

    public class UIInventory : MonoBehaviour
    {
        public UIData uiData;
        public GameObject inventoryContainerObject;
        public GameSettings gameSettings;
        private bool displayingInventory = false;

        public static GameObject Build(GameObject parent, UIData uiData)
        {
            GameObject inventoryObject = UIUtils.CreateCanvas(parent);
            inventoryObject.transform.SetParent(inventoryObject.transform);

            UIInventory uiInventory = inventoryObject.AddComponent<UIInventory>();
            uiInventory.uiData = uiData;
            uiInventory.inventoryContainerObject = inventoryObject;
            uiInventory.gameSettings = FindObjectOfType<GameController>().gameSettings;

            RectTransform rectTransform = inventoryObject.GetComponent<RectTransform>();
            rectTransform.localPosition = Vector3.zero;
            rectTransform.sizeDelta = new Vector2(500, 100);
            rectTransform.localScale = new Vector3(1, 1, 1);

            return inventoryObject;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (!displayingInventory)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            }
        }

        public void Show()
        {
            if (!displayingInventory)
            {
                DrawInventory();
                displayingInventory = true;
            }
        }

        public void Hide()
        {
            if (displayingInventory)
            {
                while (inventoryContainerObject.transform.childCount > 0)
                {
                    DestroyImmediate(inventoryContainerObject.transform.GetChild(0).gameObject);
                }
                displayingInventory = false;
            }
        }

        public void DrawInventory()
        {
            List<ItemData> inventory = Player.GetInventory().GetItems();

            // Precalculate the manipulation point (center of the inventory)
            int spriteSize = 64;
            Vector2 calculatedMidpoint = new Vector2(gameSettings.PlayerInventoryColumns * spriteSize / 2, gameSettings.PlayerInventoryRows * spriteSize / 2);
            Vector2 midleft = Vector2.zero;
            int offsetX = -(int)(calculatedMidpoint + midleft).x;
            int offsetY = (int)(calculatedMidpoint + midleft).y;

            int itemIndex = 0;
            for (int row = 0; row < gameSettings.PlayerInventoryRows; row++)
            {
                for (int col = 0; col < gameSettings.PlayerInventoryColumns; col++)
                {
                    UIUtils.CreateImage(uiData.inventorySlotSprite, inventoryContainerObject, new Vector3(offsetX, offsetY, 0), Vector3.one);
                    if (itemIndex >= 0 && itemIndex < inventory.Count)
                    {
                        UIUtils.CreateImage(inventory[itemIndex].sprite, inventoryContainerObject, new Vector3(offsetX, offsetY, 0), Vector3.one * 0.7f);
                    }
                    itemIndex++;
                    offsetX += spriteSize;
                }
                offsetX = -(int)(calculatedMidpoint + midleft).x;
                offsetY -= spriteSize;
            }
        }
    }
}