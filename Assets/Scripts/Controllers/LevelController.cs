using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using Classes;
using UnityEngine;

namespace Controllers
{
    /// <summary>
    /// Handles all interactions for switching levels
    /// </summary>
    internal class LevelController : MonoBehaviour
    {
        private const string LevelDataFileName = "levels.json";

        private int levelCount;
        private int unlockedLevels = 1;
        private int[] levelScore;
        private Dictionary<int, int[]> levelScores;

        internal int CurrentLevel { get; private set; }


        public void Start()
        {
            LoadLevels();
        }
        /// <summary>
        /// Switches current scene to required level
        /// </summary>
        /// <param name="level"></param>
        public void SwitchLevel(int level)
        {

            if (CurrentLevel == level) throw new LevelException(CurrentLevel, level);

            CurrentLevel = level;
            SceneManager.LoadScene(string.Format("level{0:D2}", level));
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
            unlockedLevels = levelCount;
            for (var i = 0; i < levelCount; i++)
            {
                if (PlayerPrefs.HasKey("Hiscore" + i))
                {
                    levelScore[i] = PlayerPrefs.GetInt("Hiscore" + i);
                }
                else
                {
                    unlockedLevels = i;
                    break;
                }
            }
        }

        public void LoadGui()
        {
            
        }

        public void UnloadGui()
        {
            Debug.Log("Unloading Level Canvas");
            Slingshot.Instance.transform.Find("LevelCanvas").gameObject.SetActive(false);
        }
    }
}