using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.EventSystem;

namespace GMTK.UI
{
    public class FishingUI : MonoBehaviour
    {
        [SerializeField] private GameObject fishingBar;
        [SerializeField] private RectTransform failBar;
        [SerializeField] private RectTransform successBar;


        // FIXME: Set this variable from the fish
        [SerializeField] private float minBarSize = 10f;
        [SerializeField] private float barIncreasedSpeed = 1f;

        private float maxBarSize = 200f;
        [SerializeField] private float barSizeMultiplier = 10f;

        private OnFinishFishingGame onFinishFishingGameEvent = Events.OnFinishFishingGame;

        private void Start()
        {
            ResetFillBars();
        }

        private void Update()
        {
            failBar.sizeDelta = new Vector2(failBar.sizeDelta.x, failBar.sizeDelta.y + barIncreasedSpeed * Time.deltaTime);
            successBar.sizeDelta = new Vector2(successBar.sizeDelta.x, successBar.sizeDelta.y + barIncreasedSpeed * Time.deltaTime);

            failBar.sizeDelta = new Vector2(failBar.sizeDelta.x, Mathf.Clamp(failBar.sizeDelta.y, minBarSize, maxBarSize));
            successBar.sizeDelta = new Vector2(successBar.sizeDelta.x, Mathf.Clamp(successBar.sizeDelta.y, minBarSize, maxBarSize));

            onFinishFishingGameEvent.successAmount = successBar.sizeDelta.y - (successBar.sizeDelta.y * barSizeMultiplier);
            onFinishFishingGameEvent.failedAmount = failBar.sizeDelta.y - (failBar.sizeDelta.y * barSizeMultiplier);
        }

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
            minBarSize = evt.minBarSize;
            barSizeMultiplier = evt.barSizeMultiplier;
            barIncreasedSpeed = evt.barIncreasedSpeed;

            ResetFillBars();
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

        private void ResetFillBars()
        {
            failBar.sizeDelta = new Vector2(failBar.sizeDelta.x, minBarSize);
            successBar.sizeDelta = new Vector2(successBar.sizeDelta.x, minBarSize);
        }
    }
}
