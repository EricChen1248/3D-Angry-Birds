using UnityEngine;
using UnityEngine.UI;

namespace Classes
{
    public class Score : MonoBehaviour
    {
        public static Score Instance;

        private int score;
        private int targetScore;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            Instance = this;
            Instance.gameObject.GetComponent<Text>().text = string.Format("{0:D}", 0);
        }

        
        public static void UpdateScore(int delta)
        {
            Instance.targetScore += delta;
            Instance.UpdateGUI();
        }

        private void UpdateGUI()
        {
            if (score >= targetScore) return;
            score += Mathf.Max((targetScore - score) / 10, 10);
            if (score >= targetScore)
            {
                score = targetScore;
            }
            Instance.gameObject.GetComponent<Text>().text = string.Format("{0:D}", score);
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void FixedUpdate()
        {
            UpdateGUI();
        }


    }
}
