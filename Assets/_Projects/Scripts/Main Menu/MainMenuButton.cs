using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GMTK
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button optionButton;
        [SerializeField] private Button creditButton;

        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            playButton.onClick.AddListener(Play);
            optionButton.onClick.AddListener(Option);
            creditButton.onClick.AddListener(Credit);

            SetCanvasGroup(true);
        }

        private void Play()
        {
            SceneManager.LoadScene("Game");
        }

        private void Option()
        {
            Debug.Log("Option");
        }

        private void Credit()
        {
            Debug.Log("Credit");
        }

        public void SetCanvasGroup(bool value)
        {
            canvasGroup.alpha = value ? 1 : 0;
            canvasGroup.interactable = value;
            canvasGroup.blocksRaycasts = value;
        }
    }
}
