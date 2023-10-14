using UnityEngine;

namespace Bunker
{
    public class UIController : MonoBehaviour
    {
        private GameSettings gameSettings;

        private void Start()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            UINotifications.Build(gameObject, gameSettings.uiData);
            UIInventory.Build(gameObject, gameSettings.uiData);
            UIHealthbar.Build(gameObject, gameSettings.uiData);
            UIWaveDefense.Build(gameObject, gameSettings.uiData);
            // UIPauseMenu.Build(gameObject, gameSettings.uiData);
        }
    }
}