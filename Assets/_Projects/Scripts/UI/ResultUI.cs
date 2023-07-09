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
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI caughtFishText;
        [SerializeField] private TextMeshProUGUI caughtTrashText;

        [SerializeField] private Button nextDayButton;
        [SerializeField] private Button mainMenuButton;

        private void Awake()
        {
            nextDayButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });

            mainMenuButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });
        }

        private void Start()
        {
            scoreText.text = ScoreManager.Instance.scoreSO.score.ToString();
            caughtFishText.text = ScoreManager.Instance.scoreSO.caughtFishCount.ToString();
            caughtTrashText.text = ScoreManager.Instance.scoreSO.caughtTrashCount.ToString();
        }
    }
}
