using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Classes;
using Classes.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    /// <summary>
    /// Handles all interactions for switching levels
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        internal static LevelController Instance;

        private const string LevelDataFileName = "levels.json";
        public Texture FilledStar;

        public int pigsCount;
        private bool gameEndInitiated;

        private int levelCount;
        private int unlockedLevels = 1;

        private int[] levelScore;
        private Dictionary<int, int[]> levelScores;
        private Dictionary<int, LevelData> levelDatas;

        internal int CurrentLevel { get; private set; }

        private void Start()
        {
            Instance = this;
            LoadLevels();
            LoadGui();
        }


        public void KillPig()
        {
            --pigsCount;
            if (pigsCount > 0 || gameEndInitiated) return;
            StartCoroutine(EndLevel());
            gameEndInitiated = true;
        }

        /// <summary>
        /// Switches current scene to required level
        /// </summary>
        /// <param name="level"></param>
        internal void SwitchLevel(int level)
        {
            if (CurrentLevel == level) throw new LevelException(CurrentLevel, level);

            CurrentLevel = level;
            pigsCount = levelDatas[level].Pigs;
            SceneManager.LoadScene(string.Format("level{0}", level));
            UnloadGui();
        }

        internal void LoadLevels()
        {
            // Loading all level datas into program
            var filePath = Path.Combine(Application.streamingAssetsPath, LevelDataFileName);
            var levelDataJSON = File.ReadAllLines(filePath);

            levelScores = new Dictionary<int, int[]>();
            levelDatas = new Dictionary<int, LevelData>();
            
            foreach (var lines in levelDataJSON)
            {
                ++levelCount;
                var levelData = JsonUtility.FromJson<LevelData>(lines);
                levelDatas.Add(levelData.Level, levelData);
                levelScores.Add(levelData.Level, new []{levelData.TwoStar, levelData.ThreeStar});
            }

            // Loading player 
            levelScore = new int[levelCount];
            unlockedLevels = 1;
            for (var i = 0; i < levelCount; i++)
            {
                if (!PlayerPrefs.HasKey("Hiscore" + i)) continue;
                var score = PlayerPrefs.GetInt("Hiscore" + i);
                levelScore[i] = score;
                ++unlockedLevels;
                if (score >= levelScores[i + 1][1])
                {
                    FillStars(i + 1, 3);
                }
                else if (score >= levelScores[i + 1][0])
                {
                    FillStars(i + 1, 2);
                }
                else
                {
                    FillStars(i + 1, 1);
                }
            }

            for (var i = 0; i < unlockedLevels; i++)
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

        private IEnumerator EndLevel()
        {

            yield return new WaitForSeconds(5f);
            var lTargetDir = Slingshot.Instance.transform.position - Camera.main.transform.position;
            while (Vector3.Angle(Camera.main.transform.forward, Slingshot.Instance.transform.position - Camera.main.transform.position) > 0f)
            {
                Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 0.1f);
                lTargetDir = Slingshot.Instance.transform.position - Camera.main.transform.position;
                Debug.Log(Vector3.Angle(Camera.main.transform.forward, Camera.main.transform.position - Slingshot.Instance.transform.position));
                yield return new WaitForSeconds(0.001f);
            }

            yield return new WaitForSeconds(1f);

            while (SlingshotPouch.Instance.CurrentAmmo != null)
            {
                SlingshotPouch.Instance.transform.localPosition = Vector3.down * 0.5f+ Vector3.forward * 0.5f;
                var ammo = SlingshotPouch.Instance.CurrentAmmo.GetComponent<Bird>();
                ammo.IsShooting = true;
                ammo.GetComponent<Rigidbody>().useGravity = true;
                Score.UpdateScore(ammo.Score);
                yield return new WaitForSeconds(2f);
            }            

            yield return new WaitForSeconds(2f);


            LoadGui();
            levelScore[CurrentLevel - 1] = Math.Max(levelScore[CurrentLevel - 1], Score.Instance.CurrentScore);
            if (levelScore[CurrentLevel - 1] >= levelScores[CurrentLevel][1])
            {
                FillStars(CurrentLevel, 3);
            }
            else if (levelScore[CurrentLevel - 1] >= levelScores[CurrentLevel][0])
            {
                FillStars(CurrentLevel, 2);
            }
            else
            {
                FillStars(CurrentLevel, 1);
            }

            Debug.Log(unlockedLevels);
            Debug.Log(CurrentLevel);
            if (unlockedLevels == CurrentLevel)
            {
                UnlockLevel();
            }

            gameEndInitiated = false;
        }

        private void UnlockLevel()
        {
            ++unlockedLevels;
            transform.Find("LevelCanvas").Find(unlockedLevels.ToString()).GetComponent<Button>().interactable = true;
        }
    }
}