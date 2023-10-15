using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Bunker
{
    public class PauseMenuController : MonoBehaviour
    {
        public UIDocument uiDocument;

        private void Start()
        {
            uiDocument.enabled = false;

        }

        private void DrawUI()
        {
            uiDocument.enabled = true;
            Button button = uiDocument.rootVisualElement.Q("Button1") as Button;
            if (button == null)
            {
                Debug.Log("button not found");
            }
            else
            {
                Debug.Log(button.name);
            }
            button.RegisterCallback<ClickEvent>(ReloadSceneOnClick);
            button.text = "Restart";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!uiDocument.enabled)
                {
                    DrawUI();
                }
            }
        }

        private void ReloadSceneOnClick(EventBase tab)
        {
            Debug.Log("Clicked");
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}