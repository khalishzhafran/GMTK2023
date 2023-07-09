using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace GMTK.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        private void Awake()
        {
            startButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });
        }
    }
}
