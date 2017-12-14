using System;
using UnityEngine;

namespace Classes
{
    [Serializable]
    public class LevelData
    {
        public int Level;
        public int TwoStar;
        public int ThreeStar;

        public static LevelData CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<LevelData>(jsonString);
        }

    }
}
