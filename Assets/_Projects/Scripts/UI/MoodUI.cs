using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.EventSystem;
using GMTK.Fisherman;
using GMTK.Misc;

namespace GMTK.UI
{
    public class MoodUI : MonoBehaviour
    {
        [SerializeField] private RectTransform moodBalanceBar;
        [SerializeField] private RectTransform moodDividerBar;
        [SerializeField] private FloatingText floatingText;
        [SerializeField] private CautionPanel moodWarning;

        [SerializeField] private float warningPercentage = 10;

        private float currentPlayerMood;

        private float satisfiedStartPosition;
        private float satisfiedWidthBar;

        private Fisher fisher;

        private void Start()
        {
            fisher = Fisher.Instance;

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
            EventManager.AddListener<OnFinishFishingGame>(OnFinishFishingGame);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnMoodChanged>(OnMoodChanged);
            EventManager.RemoveListener<OnFinishFishingGame>(OnFinishFishingGame);
        }

        private void OnMoodChanged(OnMoodChanged evt)
        {
            moodDividerBar.anchoredPosition += new Vector2(evt.moodChange, moodDividerBar.anchoredPosition.y);
            moodDividerBar.anchoredPosition = new Vector2(Mathf.Clamp(moodDividerBar.anchoredPosition.x, 0, 100), moodDividerBar.anchoredPosition.y);

            if (fisher.currentMood <= fisher.minMood + (fisher.maxMood * warningPercentage / 100) || fisher.currentMood >= fisher.maxMood - (fisher.maxMood * warningPercentage / 100))
            {
                moodWarning.Show();
            }
            else
            {
                moodWarning.Hide();
            }
        }

        private void OnFinishFishingGame(OnFinishFishingGame evt)
        {
            if (evt.isSuccessful && !evt.isTrash)
            {
                floatingText.ShowText($"+{Mathf.FloorToInt(evt.successAmount)}");
            }
            else
            {
                floatingText.ShowText($"-{Mathf.FloorToInt(evt.failedAmount)}");
            }
        }
    }
}
