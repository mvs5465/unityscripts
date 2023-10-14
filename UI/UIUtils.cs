using UnityEngine;
using UnityEngine.UI;

namespace Bunker
{
    public static class UIUtils
    {
        public static Text CreateText(Vector3 localPosition, GameObject canvasObject, UIData uiData, Color color)
        {
            GameObject textObject = new("UIText");
            textObject.transform.SetParent(canvasObject.transform);
            Text text = textObject.AddComponent<Text>();
            text.font = uiData.font;
            text.color = color;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 30;
            // text.fontStyle = FontStyle.Bold;
            RectTransform rectTransform = textObject.GetComponent<RectTransform>();
            rectTransform.localPosition = localPosition;
            rectTransform.sizeDelta = new Vector2(500, 100);
            rectTransform.localScale = Vector3.one;
            return text;
        }

        public static Image CreateImage(Sprite sprite, GameObject canvasObject, Vector3 localPosition, Vector3 localScale)
        {
            GameObject imageObject = new("UIImage");
            Image image = imageObject.AddComponent<Image>();
            image.sprite = sprite;
            imageObject.transform.SetParent(canvasObject.transform);
            RectTransform rectTransform = imageObject.GetComponent<RectTransform>();
            rectTransform.localPosition = localPosition;
            rectTransform.sizeDelta = Vector2.one * 64;
            rectTransform.localScale = localScale;
            return image;
        }

        public static GameObject CreateCanvas(GameObject parent)
        {
            GameObject uiCanvasObject = new GameObject("UICanvas");
            Camera camera = GameObject.FindObjectOfType<Camera>();
            uiCanvasObject.transform.SetParent(camera.transform);
            uiCanvasObject.transform.position = parent.transform.position;
            Canvas canvas = uiCanvasObject.AddComponent<Canvas>();
            uiCanvasObject.AddComponent<CanvasScaler>();
            uiCanvasObject.AddComponent<GraphicRaycaster>();
            canvas.worldCamera = camera;
            canvas.sortingLayerName = "UI";
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            return uiCanvasObject;
        }
    }
}