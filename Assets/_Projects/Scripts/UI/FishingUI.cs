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

        private float maxBarSize = 200f;

        private float maxGain;
        private float barIncreaseMultiplier;
        private float barIncreasedSpeedPerSecond;

        private OnFinishFishingGame onFinishFishingGameEvent = Events.OnFinishFishingGame;

        private void Start()
        {
            ResetFillBars();
        }

        private void Update()
        {
            failBar.sizeDelta = new Vector2(failBar.sizeDelta.x, failBar.sizeDelta.y + barIncreasedSpeedPerSecond * barIncreaseMultiplier * Time.deltaTime);
            successBar.sizeDelta = new Vector2(successBar.sizeDelta.x, successBar.sizeDelta.y + barIncreasedSpeedPerSecond * barIncreaseMultiplier * Time.deltaTime);

            failBar.sizeDelta = new Vector2(failBar.sizeDelta.x, Mathf.Clamp(failBar.sizeDelta.y, 0f, maxBarSize));
            successBar.sizeDelta = new Vector2(successBar.sizeDelta.x, Mathf.Clamp(successBar.sizeDelta.y, 0f, maxBarSize));

            onFinishFishingGameEvent.successAmount = successBar.sizeDelta.y / maxBarSize * maxGain;
            onFinishFishingGameEvent.failedAmount = failBar.sizeDelta.y / maxBarSize * maxGain;
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
            barIncreasedSpeedPerSecond = evt.barIncreasedSpeedPerSecond;
            maxGain = evt.maxGain;

            barIncreaseMultiplier = maxBarSize / maxGain;

            ResetFillBars();
            StartCoroutine(ShowFishingBarCoroutine());
        }

        private void OnFinishFishingGame(OnFinishFishingGame evt)
        {
            Debug.Log(successBar.sizeDelta.y / maxBarSize * maxGain);
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
            failBar.sizeDelta = new Vector2(failBar.sizeDelta.x, 0f);
            successBar.sizeDelta = new Vector2(successBar.sizeDelta.x, 0f);
        }
    }
}
