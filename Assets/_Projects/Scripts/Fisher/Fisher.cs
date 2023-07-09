using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.Managers;
using GMTK.EventSystem;

namespace GMTK.Fisherman
{
    public class Fisher : MonoBehaviour
    {
        public static Fisher Instance { get; private set; }

        public string fishermanName;
        public Sprite fishermanProfile;
        public float currentMood;
        [SerializeField] private float decreaseMoodSpeed = 1f;
        public float maxMood = 100f;
        public float minMood = 0f;

        public float satisfiedStartRange = 50f;
        public float satisfiedEndRange = 70f;


        [Header("Generate value")]
        private float minCurrentMood = 30;
        private float maxCurrentMood = 70;
        private float minSatisfiedStartRange = 50;
        private float maxSatisfiedStartRange = 70;
        private float minSatisfiedEndRange = 70;
        private float maxSatisfiedEndRange = 90;

        private bool isIdle = true;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            currentMood = Random.Range(minCurrentMood, maxCurrentMood);
            satisfiedStartRange = Random.Range(minSatisfiedStartRange, maxSatisfiedStartRange);
            satisfiedEndRange = Random.Range(minSatisfiedEndRange, maxSatisfiedEndRange);
        }



        private void OnEnable()
        {
            EventManager.AddListener<OnFinishFishingGame>(OnFinishFishingGame);
            EventManager.AddListener<OnHookedObject>(OnHookedObject);
        }



        private void OnDisable()
        {
            EventManager.RemoveListener<OnFinishFishingGame>(OnFinishFishingGame);
            EventManager.RemoveListener<OnHookedObject>(OnHookedObject);
        }



        private void Update()
        {
            if (isIdle)
            {
                ChangeMood(-decreaseMoodSpeed * Time.deltaTime);
            }
        }



        private void OnFinishFishingGame(OnFinishFishingGame evt)
        {
            isIdle = true;

            if (evt.isSuccessful)
            {
                if (!evt.isTrash)
                {
                    ChangeMood(evt.successAmount);
                    SoundManager.instance.PlaySFX(SoundManager.instance.soundEffectSO.MoodPointGained);
                    ScoreManager.Instance.scoreSO.AddCaughtFish();
                }
                else
                {
                    ChangeMood(-evt.failedAmount);
                    SoundManager.instance.PlaySFX(SoundManager.instance.soundEffectSO.MoodPointLost);
                    ScoreManager.Instance.scoreSO.AddCollectedTrash();
                }
            }
            else
            {
                ChangeMood(-evt.failedAmount);
                SoundManager.instance.PlaySFX(SoundManager.instance.soundEffectSO.MoodPointLost);
                ScoreManager.Instance.scoreSO.AddEscapedFish();
            }
        }



        private void OnHookedObject(OnHookedObject evt)
        {
            isIdle = false;
        }



        public void ChangeMood(float amount)
        {
            currentMood += amount;
            currentMood = Mathf.Clamp(currentMood, minMood, maxMood);

            OnMoodChanged evt = Events.OnMoodChanged;
            evt.moodChange = amount;
            EventManager.Broadcast(evt);
        }
    }
}
