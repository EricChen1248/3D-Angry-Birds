using UnityEngine.SceneManagement;

namespace Controllers
{
    /// <summary>
    /// Handles all interactions for switching levels
    /// </summary>
    internal static class LevelController
    {
        public static int CurrentLevel { get; private set; }
        /// <summary>
        /// Switches current scene to required level
        /// </summary>
        /// <param name="level"></param>
        public static void SwitchLevel(int level)
        {
            if (CurrentLevel == level) throw new LevelException(CurrentLevel, level);

            CurrentLevel = level;
            SceneManager.LoadScene(string.Format("level {0:D2}", level));
        }
    }
}