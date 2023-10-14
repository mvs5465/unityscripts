using UnityEngine;

namespace Bunker
{
    public class GameController : MonoBehaviour
    {
        public GameSettings gameSettings;

        private void Awake()
        {
            GameObject eventControllerObject = new GameObject();
            eventControllerObject.transform.SetParent(gameObject.transform);
        }
    }
}