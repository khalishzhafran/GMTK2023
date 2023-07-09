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
        public float currentMood;
        [SerializeField] private float decreaseMoodSpeed = 1f;
        [SerializeField] private float maxMood = 100f;
        [SerializeField] private float minMood = 0f;

        public float satisfiedStartRange = 50f;
        public float satisfiedEndRange = 70f;

        private bool isIdle = true;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
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
                    ScoreManager.Instance.scoreSO.AddScore(Mathf.FloorToInt(evt.successAmount));
                    ScoreManager.Instance.scoreSO.AddCaughtFish();
                }
                else
                {
                    ChangeMood(-evt.failedAmount);
                    ScoreManager.Instance.scoreSO.AddScore(Mathf.FloorToInt(-evt.failedAmount));
                    ScoreManager.Instance.scoreSO.AddCaughtTrash();
                }
            }
            else
            {
                ChangeMood(-evt.failedAmount);
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
