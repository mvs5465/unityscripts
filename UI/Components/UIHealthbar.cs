using UnityEngine;
using UnityEngine.UI;

namespace Bunker
{
    public class UIHealthbar : MonoBehaviour
    {
        public Image healthbarImage;
        public Text healthbarText;

        private void Start()
        {
            Player.OnPlayerHealthChange = UpdateHealth;
        }

        public void UpdateHealth(Vector2 newHealth)
        {
            healthbarImage.fillAmount = (float)newHealth.x / newHealth.y;
            healthbarText.text = newHealth.x + "/" + newHealth.y;
        }

        public static GameObject Build(GameObject parent, UIData uiData)
        {
            GameObject healthbarObject = UIUtils.CreateCanvas(parent);
            UIHealthbar healthbar = healthbarObject.AddComponent<UIHealthbar>();

            Image backImage = UIUtils.CreateImage(uiData.backgroundSprite, healthbarObject, Vector2.zero, Vector3.one).GetComponent<Image>();
            Image frontImage = UIUtils.CreateImage(uiData.healthbarSprite, healthbarObject, Vector2.zero, Vector3.one).GetComponent<Image>();
            Text overlayText = UIUtils.CreateText(Vector3.zero, healthbarObject, uiData, Color.white);

            frontImage.type = Image.Type.Filled;
            frontImage.fillMethod = Image.FillMethod.Horizontal;

            frontImage.rectTransform.localScale = new Vector3(3, 2, 1);
            backImage.rectTransform.localScale = new Vector3(3, 2, 1);

            float padding = 25;
            Vector2 topleft = new Vector2(-Screen.width / 2, Screen.height / 2);
            Vector2 adjustedPosition = topleft - new Vector2(-backImage.rectTransform.sizeDelta.x * backImage.rectTransform.localScale.x / 2 - padding, backImage.rectTransform.sizeDelta.y * backImage.rectTransform.localScale.y / 5 + padding);
            frontImage.rectTransform.anchoredPosition = adjustedPosition;
            backImage.rectTransform.anchoredPosition = adjustedPosition;

            overlayText.rectTransform.anchoredPosition = frontImage.rectTransform.anchoredPosition;
            healthbar.healthbarImage = frontImage;
            healthbar.healthbarText = overlayText;
            return healthbarObject;
        }
    }
}