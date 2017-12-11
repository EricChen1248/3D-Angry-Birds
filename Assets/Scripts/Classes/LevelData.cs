using System;
using UnityEngine;

namespace Classes
{
    [Serializable]
    internal class LevelData
    {
        internal int Level;
        internal int TwoStar;
        internal int ThreeStar;

        internal static LevelData CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<LevelData>(jsonString);
        }

    }
}
