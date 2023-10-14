using UnityEngine;

namespace Bunker
{
    public class GameController : MonoBehaviour
    {
        public GameSettings gameSettings;
        public GameEventController gameEventController = new GameEventController();

        private void Awake()
        {
            GameObject eventControllerObject = new GameObject();
            eventControllerObject.transform.SetParent(gameObject.transform);
        }
    }
}