using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryMediator 
{
    private static Dictionary<TypeResource, UnityEvent> dictionary = new Dictionary<TypeResource, UnityEvent>();
    public static void Subscribe(TypeResource _event, UnityAction action)
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


    public static void Unsubscribe(TypeResource _event, UnityAction action)
    {
        if (dictionary.TryGetValue(_event, out var collection))
        {
            collection.RemoveListener(action);
        }
    }

    public static void Publish(TypeResource type)
    {
        if (dictionary.TryGetValue(type, out var collection))
        {
            collection.Invoke();
            Debug.Log("ada:"+type);
        }
    }

}