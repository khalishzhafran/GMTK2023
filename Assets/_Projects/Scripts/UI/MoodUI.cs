using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GMTK.EventSystem;

namespace GMTK.UI
{
    public class MoodUI : MonoBehaviour
    {
        [SerializeField] private Image moodBackground;
        [SerializeField] private Image moodFillBar;
        [SerializeField] private float moodColorMultiplier = 1f;

        private void Start()
        {
            moodFillBar.fillAmount = 1f;
        }

        private void OnEnable()
        {
            EventManager.AddListener<OnMoodChanged>(OnMoodChanged);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnMoodChanged>(OnMoodChanged);
        }

        private void OnMoodChanged(OnMoodChanged evt)
        {
            moodFillBar.fillAmount = (float)evt.currentMood / (float)evt.maxMood;

            ChangeMoodColor(moodFillBar.fillAmount);
        }

        private void ChangeMoodColor(float fillAmount)
        {
            if (fillAmount > 0.5f)
            {
                moodFillBar.color = Color.Lerp(Color.yellow, Color.green, fillAmount);
                moodBackground.color = Color.Lerp(Color.yellow * moodColorMultiplier, Color.green * moodColorMultiplier, fillAmount * moodColorMultiplier);
            }
            else if (fillAmount > 0.3f)
            {
                moodFillBar.color = Color.Lerp(Color.red, Color.yellow, fillAmount);
                moodBackground.color = Color.Lerp(Color.red * moodColorMultiplier, Color.yellow * moodColorMultiplier, fillAmount * moodColorMultiplier);
            }
            else
            {
                moodFillBar.color = Color.Lerp(Color.red, Color.red, fillAmount);
                moodBackground.color = Color.Lerp(Color.red * moodColorMultiplier, Color.red * moodColorMultiplier, fillAmount * moodColorMultiplier);
            }
        }
    }
}
