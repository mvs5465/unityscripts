using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bunker
{
    public class TerminalController : InteractableController
    {
        public List<GameObject> unlockableItems;
        private List<GameObject> runtimeUnlockableItems = new();
        private GameSettings gameSettings;

        private bool displaying = false;
        private GameObject canvasObject;

        protected override void StartCall()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            foreach (GameObject prefab in unlockableItems)
            {
                runtimeUnlockableItems.Add(prefab);
            }
        }

        protected override void Interact()
        {
            if (!displaying)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            canvasObject = CreateInteractableCanvas();
            DrawItemList(canvasObject);
            displaying = true;
        }

        private void Hide()
        {
            Destroy(canvasObject);
            displaying = false;
        }

        private void ClickItem(GameObject prefab, ItemData itemData)
        {
            Debug.Log($"Clicked {itemData.itemName}");
            Instantiate(prefab, transform.position, Quaternion.identity);
            runtimeUnlockableItems.Remove(prefab);
            Hide();
            Show();
        }

        private void DrawItemList(GameObject canvasObject)
        {
            int offset = 0;
            foreach (GameObject prefab in runtimeUnlockableItems)
            {
                ItemData itemData = prefab.GetComponent<ItemPrefabController>().itemData;
                Vector3 imgPos = Vector3.down * offset;
                UIUtils.CreateImage(gameSettings.uiData.inventorySlotSprite, canvasObject, imgPos, Vector3.one * 1.1f);
                Image image = UIUtils.CreateImage(itemData.sprite, canvasObject, imgPos, Vector3.one);
                Button button = image.gameObject.AddComponent<Button>();
                button.interactable = true;
                button.onClick.AddListener(() =>
                {
                    ClickItem(prefab, itemData);
                });
                offset += 64;
            }
        }

        private GameObject CreateInteractableCanvas()
        {
            GameObject canvasObject = UIUtils.CreateCanvas(gameObject);
            canvasObject.name = "TerminalCanvas";
            canvasObject.AddComponent<EventSystem>();
            canvasObject.AddComponent<StandaloneInputModule>();
            return canvasObject;
        }

        protected override void Deactivate()
        {
        }

        protected override void Activate() { }
    }
}