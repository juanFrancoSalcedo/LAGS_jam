using System.Collections.Generic;
using UnityEngine.Events;

public class MiningMediator
{
    private static Dictionary<TypeMiningEvent,UnityEvent> dictionary = new Dictionary<TypeMiningEvent, UnityEvent> ();
    public static void Subscribe(TypeMiningEvent _event, UnityAction action) 
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

    public static void Unsubscribe(TypeMiningEvent _event, UnityAction action)
    {
        if (dictionary.TryGetValue(_event, out var collection))
        {
            collection.RemoveListener(action);
        }
    }

    public static void Publish(TypeMiningEvent type) 
    {
        if (dictionary.TryGetValue(type, out var collection))
        {
            collection.Invoke();
        }
    }
}
