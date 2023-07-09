using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GMTK.EventSystem;

namespace GMTK.UI
{
    public class FishingUI : MonoBehaviour
    {
        [SerializeField] private GameObject fishingBar;
        [SerializeField] private RectTransform failBar;
        [SerializeField] private RectTransform successBar;

        [Space(10)]
        [Header("Power Bar")]
        [SerializeField] private GameObject powerBar;
        [SerializeField] private Image powerBarBackground;
        [SerializeField] private Image powerBarFill;
        private float powerColorMultiplier = 0.8f;

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
            EventManager.AddListener<OnReeling>(OnReeling);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnHookedObject>(OnHookedObject);
            EventManager.RemoveListener<OnFinishFishingGame>(OnFinishFishingGame);
            EventManager.RemoveListener<OnReeling>(OnReeling);
        }

        private void OnHookedObject(OnHookedObject evt)
        {
            barIncreasedSpeedPerSecond = evt.moodGainSpeedPerSecond;
            maxGain = evt.maxGain;

            barIncreaseMultiplier = maxBarSize / maxGain;

            ResetFillBars();
            StartCoroutine(ShowBarCoroutine());
        }

        private void OnFinishFishingGame(OnFinishFishingGame evt)
        {
            HideBar();
        }

        private void OnReeling(OnReeling evt)
        {
            powerBarFill.fillAmount = (float)evt.fishPower / (float)evt.maxFishPower;

            ChangePowerFillColor(powerBarFill.fillAmount);
        }

        private IEnumerator ShowBarCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            fishingBar.SetActive(true);
            powerBar.SetActive(true);
        }

        private void ChangePowerFillColor(float fillAmount)
        {
            if (fillAmount <= 0.5f)
            {
                powerBarFill.color = Color.Lerp(Color.green, Color.yellow, fillAmount);
                powerBarBackground.color = Color.Lerp(Color.green * powerColorMultiplier, Color.yellow * powerColorMultiplier, fillAmount * powerColorMultiplier);
            }
            else if (fillAmount > 0.5f && fillAmount <= 0.7f)
            {
                powerBarFill.color = Color.Lerp(Color.red, Color.yellow, fillAmount);
                powerBarBackground.color = Color.Lerp(Color.red * powerColorMultiplier, Color.yellow * powerColorMultiplier, fillAmount * powerColorMultiplier);
            }
            else
            {
                powerBarFill.color = Color.Lerp(Color.red, Color.red, fillAmount);
                powerBarBackground.color = Color.Lerp(Color.red * powerColorMultiplier, Color.red * powerColorMultiplier, fillAmount * powerColorMultiplier);
            }
        }

        private void HideBar()
        {
            fishingBar.SetActive(false);
            powerBar.SetActive(false);
        }

        private void ResetFillBars()
        {
            failBar.sizeDelta = new Vector2(failBar.sizeDelta.x, 0f);
            successBar.sizeDelta = new Vector2(successBar.sizeDelta.x, 0f);

            powerBarFill.fillAmount = 0f;
            powerBarFill.color = Color.Lerp(Color.green, Color.yellow, powerBarFill.fillAmount);
            powerBarBackground.color = Color.Lerp(Color.green * powerColorMultiplier, Color.yellow * powerColorMultiplier, powerBarFill.fillAmount * powerColorMultiplier);
        }
    }
}
