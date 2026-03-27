using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateMediator 
{
    private static Dictionary<TypeGameState, UnityEvent> dictionary = new Dictionary<TypeGameState, UnityEvent>();
    public static void Subscribe(TypeGameState _event, UnityAction action)
    {
        if (dictionary.TryGetValue(_event, out var collection))
        {
            collection.AddListener(action);
        }
        else
        {
            var thisEvent = new UnityEvent();
            thisEvent.AddListener(action);
            dictionary.Add(_event, thisEvent);
        }
    }


    public static void Unsubscribe(TypeGameState _event, UnityAction action)
    {
        if (dictionary.TryGetValue(_event, out var collection))
        {
            collection.RemoveListener(action);
        }
    }

    public static void Publish(TypeGameState type)
    {
        if (dictionary.TryGetValue(type, out var collection))
        {
            collection.Invoke();
            Debug.Log("ada:" + type);
        }
    }
}

public enum TypeGameState 
{
    None,
    Welcome,
    StartDay,
    EndDay,
    FinishGame
}
