using System;
using UnityEngine;
using UnityEngine.Events;

public class GameStateListener : MonoBehaviour
{
    [SerializeField] private TypeGameState gameState;
    [SerializeField] private UnityEvent eventGame;
    private void OnEnable()
    {
        GameStateMediator.Subscribe(gameState,CallEvent);
    }
    private void OnDisable()
    {
        GameStateMediator.Unsubscribe(gameState,CallEvent);
    }
    private void CallEvent()
    {
        print("Hardaman");
        eventGame?.Invoke();
    }
}
