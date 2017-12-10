using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    /// <summary>
    /// Handles all interactions for switching levels
    /// </summary>
    internal class LevelController : MonoBehaviour
    {
        private const string LevelDataFileName = "levels.json";
        public Texture FilledStar;

        private int levelCount;
        private int unlockedLevels = 1;
        private int[] levelScore;
        private Dictionary<int, int[]> levelScores;

        internal int CurrentLevel { get; private set; }

        public void Start()
        {
            LoadLevels();
            LoadGui();
        }
        /// <summary>
        /// Switches current scene to required level
        /// </summary>
        /// <param name="level"></param>
        public void SwitchLevel(int level)
        {
            if (CurrentLevel == level) throw new LevelException(CurrentLevel, level);

            CurrentLevel = level;
            SceneManager.LoadScene(string.Format("level{0}", level));
        }

        internal void LoadLevels()
        {
            // Loading all level datas into program
            var filePath = Path.Combine(Application.streamingAssetsPath, LevelDataFileName);
            var levelDataJSON = File.ReadAllLines(filePath);
            levelScores = new Dictionary<int, int[]>();
            
            foreach (var lines in levelDataJSON)
            {
                ++levelCount;
                var levelData = JsonUtility.FromJson<LevelData>(lines);
                levelScores.Add(levelData.Level, new []{levelData.TwoStar, levelData.ThreeStar});
            }

            // Loading player 
            levelScore = new int[levelCount];
            unlockedLevels = levelCount - 1;
            for (var i = 0; i < levelCount; i++)
            {
                if (PlayerPrefs.HasKey("Hiscore" + i))
                {
                    var score = PlayerPrefs.GetInt("Hiscore" + i);
                    levelScore[i] = score;
                    if (score >= levelScores[i + 1][1])
                    {
                        FillStars(i + 1, 3);
                    }
                    else if (score >= levelScores[i + 1][0])
                    {
                        FillStars(i + 1, 2);
                    }
                }
                else
                {
                    unlockedLevels = i;
                    break;
                }
            }

            for (var i = 0; i <= unlockedLevels; i++)
            {
                transform.Find("LevelCanvas").Find((i + 1).ToString()).GetComponent<Button>().interactable = true;
            }
        }

        public void LoadGui()
        {
            transform.Find("LevelCanvas").gameObject.SetActive(true);
        }

        public void UnloadGui()
        {
            transform.Find("LevelCanvas").gameObject.SetActive(false);
        }

        private void FillStars(int level, int stars)
        {
            var images = transform.Find("LevelCanvas").Find(level.ToString()).Find("Stars");
            for (var i = 0; i < stars; i++)
            {
                images.Find("Star" + i).GetComponent<RawImage>().texture = FilledStar;
            }
        }
    }
}