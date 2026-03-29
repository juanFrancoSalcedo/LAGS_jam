using System;
using UnityEngine;
using UnityEngine.Events;

public class GameStateListener : MonoBehaviour
{
    [SerializeField] private TypeGameState gameState;
    [SerializeField] private UnityEvent eventGame;
    private void OnEnable() => GameStateContext.GameStateMediator.Subscribe(gameState, CallEvent);
    private void OnDisable() => GameStateContext.GameStateMediator.Unsubscribe(gameState, CallEvent);
    private void CallEvent() => eventGame?.Invoke();
}
