using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using GMTK.EventSystem;
using GMTK.Core;

namespace GMTK.UI
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private DaySO day;

        private void Start()
        {
            dayText.text = "Day " + day.currentDay.ToString();
        }

        private void OnEnable()
        {
            EventManager.AddListener<OnTimeChanged>(OnTimeChanged);
            EventManager.AddListener<OnDayChanged>(OnDayChanged);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnTimeChanged>(OnTimeChanged);
            EventManager.RemoveListener<OnDayChanged>(OnDayChanged);
        }

        private void OnTimeChanged(OnTimeChanged evt)
        {
            string formattedTime = evt.hours.ToString("00") + ":" + evt.minutes.ToString("00");

            timeText.text = formattedTime;
        }

        private void OnDayChanged(OnDayChanged evt)
        {
            dayText.text = "Day " + evt.currentDay.ToString();
        }
    }
}
