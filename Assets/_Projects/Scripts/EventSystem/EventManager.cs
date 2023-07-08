using System;
using System.Collections.Generic;

namespace GMTK.EventSystem
{
    public static class EventManager
    {
        private static readonly Dictionary<Type, Action<GameEvent>> eventCollections = new Dictionary<Type, Action<GameEvent>>();
        private static readonly Dictionary<Delegate, Action<GameEvent>> eventLookups = new Dictionary<Delegate, Action<GameEvent>>();

        public static void AddListener<T>(Action<T> evt) where T : GameEvent
        {
            if (!eventLookups.ContainsKey(evt))
            {
                Action<GameEvent> newAction = (e) => evt((T)e);
                eventLookups[evt] = newAction;

                if (eventCollections.TryGetValue(typeof(T), out Action<GameEvent> existingAction))
                {
                    eventCollections[typeof(T)] = existingAction += newAction;
                }
                else
                {
                    eventCollections[typeof(T)] = newAction;
                }
            }
        }

        public static void RemoveListener<T>(Action<T> evt) where T : GameEvent
        {
            if (eventLookups.TryGetValue(evt, out var action))
            {
                if (eventCollections.TryGetValue(typeof(T), out var existingAction))
                {
                    existingAction -= action;
                    if (existingAction == null)
                    {
                        eventCollections.Remove(typeof(T));
                    }
                    else
                    {
                        eventCollections[typeof(T)] = existingAction;
                    }
                }

                eventLookups.Remove(evt);
            }
        }

        public static void Broadcast(GameEvent evt)
        {
            if (eventCollections.TryGetValue(evt.GetType(), out var action))
            {
                action.Invoke(evt);
            }
        }

        public static void Clear()
        {
            eventCollections.Clear();
            eventLookups.Clear();
        }
    }
}
