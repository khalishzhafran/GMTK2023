using UnityEngine;

namespace GMTK.EventSystem
{
    public class GameEvent { public GameEvent() { } }

    public class OnMoodChanged : GameEvent
    {
        public float currentMood;
        public float maxMood;
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
    }

    public class OnFinishFishingGame : GameEvent
    {
        public OnFinishFishingGame() { }
    }
}
