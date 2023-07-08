using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GMTK.EventSystem;

namespace GMTK.UI
{
    public class MoodUI : MonoBehaviour
    {
        [SerializeField] private RectTransform moodBalanceBar;
        [SerializeField] private RectTransform moodDividerBar;

        [SerializeField] private float satisfiedStartPosition = 50f; // Get this from the player
        [SerializeField] private float satisfiedWidthBar = 40f; // Get this from the player
        [SerializeField] private float moodDividerMaxPosition = 50;
        [SerializeField] private float moodDividerMinPosition = -50f;

        private int centerOfBalanceBar;

        private void Start()
        {
            moodBalanceBar.anchoredPosition = new Vector2(satisfiedStartPosition, moodBalanceBar.anchoredPosition.y);
            moodBalanceBar.sizeDelta = new Vector2(satisfiedWidthBar, moodBalanceBar.sizeDelta.y);

            centerOfBalanceBar = Mathf.FloorToInt((moodBalanceBar.sizeDelta.x / 2f) - (moodDividerBar.sizeDelta.x / 2f)) + 1;
            moodDividerBar.anchoredPosition = new Vector2(centerOfBalanceBar, moodDividerBar.anchoredPosition.y);
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
            moodDividerBar.anchoredPosition = new Vector2(Mathf.Clamp(moodDividerBar.anchoredPosition.x, moodDividerMinPosition, moodDividerMaxPosition), moodDividerBar.anchoredPosition.y);
        }
    }
}
