using UnityEngine;

public class TimerHandler:MonoBehaviour
{
    [SerializeField] Timer timer;

    private void OnEnable()
    {
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.StartDay, StartTimer);
        timer.OnTimeCompleted += StopTimer;
    }

    private void StartTimer() => timer.StartTimer();

    private void StopTimer() 
    {
        timer.StopTimer();
        GameStateContext.ChangeState(TypeGameState.EndDay);
    }

    private void OnDisable()
    {
        GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.StartDay, StartTimer);
        timer.OnTimeCompleted -= StopTimer;
    }
}
