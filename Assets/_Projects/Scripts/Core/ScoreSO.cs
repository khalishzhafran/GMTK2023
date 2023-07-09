using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK.Core
{
    [CreateAssetMenu(menuName = "GMTK/Core/ScoreSO")]
    public class ScoreSO : ScriptableObject
    {
        public int caughtFishCount = 0;
        public int escapedFishCount = 0;
        public int collectTrashCount = 0;
        public float currentPlayerMood = 0f;
        public float satisfiedStartRange = 0f;
        public float satisfiedEndRange = 0f;

        public void AddCaughtFish()
        {
            caughtFishCount++;
        }

        public void AddEscapedFish()
        {
            escapedFishCount++;
        }

        public void AddCollectedTrash()
        {
            collectTrashCount++;
        }

        public void ResetScore()
        {
            caughtFishCount = 0;
            escapedFishCount = 0;
            collectTrashCount = 0;
            currentPlayerMood = 0f;
            satisfiedStartRange = 0f;
            satisfiedEndRange = 0f;
        }
    }
}
