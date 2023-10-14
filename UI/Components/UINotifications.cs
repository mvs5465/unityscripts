using UnityEngine;
using UnityEngine.UI;

namespace Bunker
{
    public class UINotifications : MonoBehaviour
    {
        public class UINotificationEventSubscriber : GameEventSubscriber
        {
            private UINotifications uiNotifications;
            public UINotificationEventSubscriber(UINotifications uiNotifications) => this.uiNotifications = uiNotifications;
            public override void HandleEvent(GameEvent gameEvent)
            {
                if (gameEvent is UINotificationEvent uiNotificationEvent)
                {
                    uiNotifications.DisplayAlert(uiNotificationEvent.message);
                }
            }
        }

        public Text alertText;
        public GameSettings gameSettings;

        private void Awake()
        {
            FindObjectOfType<GameController>().gameEventController.Subscribe(new UINotificationEventSubscriber(this));
        }

        public void DisplayAlert(string message)
        {
            DisplayAlert(message, 0);
        }

        public void DisplayAlert(string message, float persistTime)
        {
            if (persistTime == 0) persistTime = 2.5f;
            CancelInvoke("ClearAlert");
            alertText.text = message;
            Invoke("ClearAlert", persistTime);
        }

        private void ClearAlert()
        {
            alertText.text = "";
        }

        public static GameObject Build(GameObject parent, UIData uiData)
        {
            GameObject canvasObject = UIUtils.CreateCanvas(parent);
            UINotifications uiNotifications = canvasObject.AddComponent<UINotifications>();
            uiNotifications.alertText = UIUtils.CreateText(new Vector3(0, 100, 0), canvasObject, uiData, Color.yellow);
            uiNotifications.gameSettings = FindObjectOfType<GameController>().gameSettings;
            return canvasObject;
        }
    }
}