using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Bunker
{
    public class MainMenu : MonoBehaviour
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
            if (button.name == "Start")
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}