using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using GMTK.Managers;

namespace GMTK.UI
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fishCaughtText;
        [SerializeField] private TextMeshProUGUI fishEscapedText;
        [SerializeField] private TextMeshProUGUI trashCollectedText;

        [SerializeField] private RectTransform moodBalanceBar;
        [SerializeField] private RectTransform moodDividerBar;

        [SerializeField] private float satisfiedStartPosition;
        [SerializeField] private float satisfiedWidthBar;

        private float moodBarMultiplier = 6f;

        [SerializeField] private Button nextDayButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            nextDayButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });

            mainMenuButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });
        }

        private void Start()
        {
            fishCaughtText.text = $"Fish Caught Count : {ScoreManager.Instance.scoreSO.caughtFishCount}";
            fishEscapedText.text = $"Fish Escaped Count : {ScoreManager.Instance.scoreSO.escapedFishCount}";
            trashCollectedText.text = $"Trash Collected Count : {ScoreManager.Instance.scoreSO.collectTrashCount}";

            satisfiedStartPosition = ScoreManager.Instance.scoreSO.satisfiedStartRange;
            satisfiedWidthBar = ScoreManager.Instance.scoreSO.satisfiedEndRange - ScoreManager.Instance.scoreSO.satisfiedStartRange;

            moodBalanceBar.anchoredPosition = new Vector2(satisfiedStartPosition * moodBarMultiplier, moodBalanceBar.anchoredPosition.y);
            moodBalanceBar.sizeDelta = new Vector2(satisfiedWidthBar * moodBarMultiplier, moodBalanceBar.sizeDelta.y);

            moodDividerBar.anchoredPosition = new Vector2(ScoreManager.Instance.scoreSO.currentPlayerMood * moodBarMultiplier, moodDividerBar.anchoredPosition.y);
        }
    }
}
