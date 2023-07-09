using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK.Core
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        public ScoreSO scoreSO;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}
