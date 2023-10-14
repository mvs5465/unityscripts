using UnityEngine;
using UnityEngine.UI;

namespace Bunker
{
    public class UIWaveDefense : MonoBehaviour
    {
        public Text scoreText;
        private int currentScore;

        private void Start()
        {
            BaseDefenseController.OnScoreEvent += UpdateScore;
        }

        public void UpdateScore(int newScore)
        {
            currentScore += newScore;
            scoreText.text = " Wave: " + (currentScore / 10 + 1) + "\nScore: " + currentScore;
        }

        public static GameObject Build(GameObject parent, UIData uiData)
        {
            GameObject scoreObject = UIUtils.CreateCanvas(parent);
            UIWaveDefense waveDefense = scoreObject.AddComponent<UIWaveDefense>();
            Text overlayText = UIUtils.CreateText(Vector3.zero, scoreObject, uiData, Color.white);
            waveDefense.scoreText = overlayText;
            waveDefense.scoreText.text = " Wave: 1\nScore: 0";
            Vector2 midLeft = new Vector2(-Screen.width / 2, 0) + new Vector2(overlayText.preferredWidth, 0);
            overlayText.rectTransform.anchoredPosition = midLeft;
            return scoreObject;
        }
    }
}