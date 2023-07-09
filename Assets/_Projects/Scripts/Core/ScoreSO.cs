using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK.Core
{
    [CreateAssetMenu(menuName = "GMTK/Core/ScoreSO")]
    public class ScoreSO : ScriptableObject
    {
        public int score = 0;
        public int caughtFishCount = 0;
        public int caughtTrashCount = 0;

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
        }

        public void AddCaughtFish()
        {
            caughtFishCount++;
        }

        public void AddCaughtTrash()
        {
            caughtTrashCount++;
        }
    }
}