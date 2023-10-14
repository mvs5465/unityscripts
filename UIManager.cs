using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections;

namespace Bunker {
    public class UIManager : MonoBehaviour
    {
        public Player player;

        private Label healthLabel;
        private Label killCountLabel;

        void OnEnable()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
            healthLabel = root.Q<Label>("HealthLabel");
            killCountLabel = root.Q<Label>("KillCountLabel");
        }

        void Update()
        {
            healthLabel.text = "Health: " + player.GetHealth().ToString();
            killCountLabel.text = "Kills: " + player.GetKillCount().ToString();
        }
    }
}