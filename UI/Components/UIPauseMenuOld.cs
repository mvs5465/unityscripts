
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bunker
{
    public class UIButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("You clicked me");
        }
    }

    public class UIPauseMenuOld : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.GetComponent<Canvas>().enabled = !gameObject.GetComponent<Canvas>().enabled;
                FindObjectOfType<UIInventory>().Hide();
                //Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            }
        }

        public static GameObject Build(GameObject parent, UIData uiData)
        {
            GameObject canvasObject = UIUtils.CreateCanvas(parent);
            UIPauseMenuOld uiPauseMenu = canvasObject.AddComponent<UIPauseMenuOld>();
            Image backImg = UIUtils.CreateImage(uiData.inventoryBackgroundSprite, canvasObject, Vector3.zero, new Vector2(3, 4)).GetComponent<Image>();
            Text tileText = UIUtils.CreateText(new Vector3(0, 100, 0), canvasObject, uiData, Color.black);
            tileText.text = "Title Text";
            Text resumeText = UIUtils.CreateText(new Vector3(0, 0, 0), canvasObject, uiData, Color.black);
            // resumeText.text = "Resume Text";
            Text restartText = UIUtils.CreateText(new Vector3(0, -100, 0), canvasObject, uiData, Color.black);
            restartText.text = "Resume Text";
            canvasObject.GetComponent<Canvas>().enabled = false;

            GameObject newButton = new();
            newButton.AddComponent<Button>();
            newButton.transform.SetParent(canvasObject.transform);
            newButton.transform.localPosition = Vector3.zero;
            newButton.GetComponent<RectTransform>().localScale = Vector3.one;

            newButton.AddComponent<EventSystem>();
            UIButton uiButton = backImg.AddComponent<UIButton>();
            // Button testButton = resumeText.gameObject.AddComponent<Button>();
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log("Clicked the title text");
            });
            return canvasObject;
        }
    }
}