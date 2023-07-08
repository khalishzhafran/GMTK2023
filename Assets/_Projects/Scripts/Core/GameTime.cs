using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GMTK.Core
{
    public class GameTime : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI dayText;

        [SerializeField] private float startHour = 8f; // The hour the game starts at
        [SerializeField] private float minutesPerSecond = 60f; // How many in-game minutes pass per real-time second
        [SerializeField] private float minutesFormatMultiplier = 10f; // How many in-game minutes pass per real-time second
        [SerializeField] private float hoursPerMinute = 60f; // How many in-game hours pass per in-game minute
        [SerializeField] private float hoursFormatMultiplier = 10f; // How many in-game hours pass per in-game minute
        [SerializeField] private float daysPerHour = 24f; // How many in-game days pass per in-game hour
        private int currentDay = 1;
        private float currentTime = 0f;
        private float startingTime; // The time the game starts at
        private float dayDuration; // Duration of each in-game day in seconds

        private void Start()
        {
            dayDuration = hoursPerMinute * minutesPerSecond * daysPerHour;

            // Set the time to the start hour
            startingTime = startHour * hoursPerMinute * minutesPerSecond;
            currentTime = startingTime;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;

            if (currentTime >= dayDuration)
            {
                currentTime = startingTime;
                currentDay++;

                dayText.text = "Day\n" + currentDay.ToString();
                // You can trigger events here when a new day starts
            }

            // Format the time as hours and minutes
            int hours = Mathf.FloorToInt(currentTime / (hoursPerMinute * minutesPerSecond)) * Mathf.FloorToInt(hoursFormatMultiplier);
            int minutes = Mathf.FloorToInt((currentTime % (hoursPerMinute * minutesPerSecond)) / minutesPerSecond) * Mathf.FloorToInt(minutesFormatMultiplier);

            string formattedTime = hours.ToString("00") + ":" + minutes.ToString("00");

            timeText.text = formattedTime;
        }
    }
}