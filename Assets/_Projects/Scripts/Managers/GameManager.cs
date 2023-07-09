using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GMTK.Fisherman;

namespace GMTK.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float maxStackMoodTimer = 2f;

        private float currentStackMoodTimer = 0f;

        private void Start()
        {
            ScoreManager.Instance.scoreSO.ResetScore();
        }

        private void Update()
        {
            if (Fisher.Instance.currentMood == Fisher.Instance.minMood || Fisher.Instance.currentMood == Fisher.Instance.maxMood)
            {
                currentStackMoodTimer += Time.deltaTime;

                if (currentStackMoodTimer >= maxStackMoodTimer)
                {
                    currentStackMoodTimer = 0f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}
