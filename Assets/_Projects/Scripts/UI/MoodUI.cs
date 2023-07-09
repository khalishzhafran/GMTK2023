using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using GMTK.EventSystem;
using GMTK.Fisherman;

namespace GMTK.UI
{
    public class MoodUI : MonoBehaviour
    {
        [SerializeField] private RectTransform moodBalanceBar;
        [SerializeField] private RectTransform moodDividerBar;

        [SerializeField] private TextMeshProUGUI fishermanName;

        private float currentPlayerMood;

        private float satisfiedStartPosition;
        private float satisfiedWidthBar;

        private Fisher fisher;

        private void Start()
        {
            fisher = Fisher.Instance;

            fishermanName.text = fisher.fishermanName;

            satisfiedStartPosition = fisher.satisfiedStartRange;
            satisfiedWidthBar = fisher.satisfiedEndRange - fisher.satisfiedStartRange;
            currentPlayerMood = fisher.currentMood;

            moodBalanceBar.anchoredPosition = new Vector2(satisfiedStartPosition, moodBalanceBar.anchoredPosition.y);
            moodBalanceBar.sizeDelta = new Vector2(satisfiedWidthBar, moodBalanceBar.sizeDelta.y);

            moodDividerBar.anchoredPosition = new Vector2(moodDividerBar.anchoredPosition.x + currentPlayerMood, moodDividerBar.anchoredPosition.y);
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
            moodDividerBar.anchoredPosition += new Vector2(evt.moodChange, moodDividerBar.anchoredPosition.y);
            moodDividerBar.anchoredPosition = new Vector2(Mathf.Clamp(moodDividerBar.anchoredPosition.x, 0, 100), moodDividerBar.anchoredPosition.y);
        }
    }
}
