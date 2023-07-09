using UnityEngine;

namespace GMTK.EventSystem
{
    public class GameEvent { public GameEvent() { } }

    public class OnMoodChanged : GameEvent
    {
        public float moodChange;
    }

    public class OnDayChanged : GameEvent
    {
        public int currentDay;
    }

    public class OnTimeChanged : GameEvent
    {
        public int hours;
        public int minutes;
    }

    public class OnHookedObject : GameEvent
    {
        public Transform hookedObject;
        public float barIncreasedSpeedPerSecond;
        public float maxGain;
    }

    public class OnFinishFishingGame : GameEvent
    {
        public float successAmount;
        public float failedAmount;
    }
}
