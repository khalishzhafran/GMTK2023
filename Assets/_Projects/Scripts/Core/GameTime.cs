using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.EventSystem;
using GMTK.Fisherman;

namespace GMTK.Core
{
    public class GameTime : MonoBehaviour
    {
        [SerializeField] private float startHour = 8f; // The hour the game starts at
        [SerializeField] private float secondsPerMinute = 60f; // How many in-game minutes pass per real-time second
        [SerializeField] private float minutesFormatMultiplier = 10f; // How many in-game minutes pass per real-time second
        [SerializeField] private float minusPerHour = 60f; // How many in-game hours pass per in-game minute
        [SerializeField] private float hoursFormatMultiplier = 10f; // How many in-game hours pass per in-game minute
        [SerializeField] private float HoursPerDay = 24f; // How many in-game days pass per in-game hour
        [SerializeField] private DaySO day;
        private float currentTime = 0f;
        private float startingTime; // The time the game starts at
        private float dayDuration; // Duration of each in-game day in seconds

        private void Start()
        {
            dayDuration = minusPerHour * secondsPerMinute * HoursPerDay;

            // Set the time to the start hour
            startingTime = startHour * minusPerHour * secondsPerMinute;
            currentTime = startingTime;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;

            if (currentTime >= dayDuration)
            {
                currentTime = startingTime;
                day.currentDay++;

                OnDayChanged evt = Events.OnDayChanged;
                evt.currentDay = day.currentDay;
                evt.currentPlayerMood = Fisher.Instance.currentMood;
                evt.satisfiedStartRange = Fisher.Instance.satisfiedStartRange;
                evt.satisfiedEndRange = Fisher.Instance.satisfiedEndRange;
                EventManager.Broadcast(evt);
                // You can trigger events here when a new day starts
            }

            // Format the time as hours and minutes
            int hours = Mathf.FloorToInt(currentTime / (minusPerHour * secondsPerMinute)) * Mathf.FloorToInt(hoursFormatMultiplier);
            int minutes = Mathf.FloorToInt((currentTime % (minusPerHour * secondsPerMinute)) / secondsPerMinute) * Mathf.FloorToInt(minutesFormatMultiplier);

            OnTimeChanged timeChangedEvt = Events.OnTimeChanged;
            timeChangedEvt.hours = hours;
            timeChangedEvt.minutes = minutes;
            EventManager.Broadcast(timeChangedEvt);
        }
    }
}
