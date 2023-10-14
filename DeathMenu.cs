using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace Bunker
{
    public class DeathMenu : MonoBehaviour
    {
        void OnEnable()
        {
            GetComponent<UIDocument>().rootVisualElement.Query<Button>().ForEach((button) =>
            {
                button.clickable.clickedWithEventInfo += LoadNextScene;
            });
        }

        private void LoadNextScene(EventBase tab)
        {
            Button button = tab.target as Button;
            if (button.name == "MainMenu")
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}