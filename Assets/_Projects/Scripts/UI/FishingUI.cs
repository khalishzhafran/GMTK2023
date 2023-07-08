using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.EventSystem;

namespace GMTK.UI
{
    public class FishingUI : MonoBehaviour
    {
        [SerializeField] private GameObject fishingBar;

        private void OnEnable()
        {
            EventManager.AddListener<OnHookedObject>(OnHookedObject);
            EventManager.AddListener<OnFinishFishingGame>(OnFinishFishingGame);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnHookedObject>(OnHookedObject);
            EventManager.RemoveListener<OnFinishFishingGame>(OnFinishFishingGame);
        }

        private void OnHookedObject(OnHookedObject evt)
        {
            StartCoroutine(ShowFishingBarCoroutine());
        }

        private void OnFinishFishingGame(OnFinishFishingGame evt)
        {
            HideFishingBar();
        }

        private IEnumerator ShowFishingBarCoroutine()
        {
            yield return new WaitForSeconds(0.8f);
            fishingBar.SetActive(true);
        }

        private void HideFishingBar()
        {
            fishingBar.SetActive(false);
        }
    }
}
