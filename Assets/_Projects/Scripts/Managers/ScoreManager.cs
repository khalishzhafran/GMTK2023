using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GMTK.Core;
using GMTK.EventSystem;

namespace GMTK.Managers
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

        private void OnEnable()
        {
            EventManager.AddListener<OnDayChanged>(OnDayChanged);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnDayChanged>(OnDayChanged);
        }

        private void OnDayChanged(OnDayChanged evt)
        {
            scoreSO.currentPlayerMood = evt.currentPlayerMood;
            scoreSO.satisfiedStartRange = evt.satisfiedStartRange;
            scoreSO.satisfiedEndRange = evt.satisfiedEndRange;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
