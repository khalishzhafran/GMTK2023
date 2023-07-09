using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.EventSystem;

namespace GMTK.Fisherman
{
    public class Fisher : MonoBehaviour
    {
        public float currentMood;
        [SerializeField] private float maxMood = 100f;
        [SerializeField] private float minMood = 0f;

        public float satisfiedStartRange = 50f;
        public float satisfiedEndRange = 70f;



        private void Update()
        {
            // Dev Only
            TestingMood();
        }


        #region Dev Only
        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Mood: " + currentMood);
        }



        private void TestingMood()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ChangeMood(10f);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ChangeMood(-10f);
            }
        }
        #endregion



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
