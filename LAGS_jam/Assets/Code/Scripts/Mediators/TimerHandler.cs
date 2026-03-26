using UnityEngine;

public class TimerHandler:MonoBehaviour
{
    [SerializeField] Timer timer;

    private void OnEnable()
    {
        GameStateMediator.Subscribe(TypeGameState.StartDay, StartTimer);
        timer.OnTimeCompleted += StopTimer;
    }

    private void StartTimer()
    {
        timer.StartTimer();
    }

    private void StopTimer() 
    {
        timer.StopTimer();
        GameStateMediator.Publish(TypeGameState.EndDay);
    }

    private void OnDisable()
    {
        GameStateMediator.Unsubscribe(TypeGameState.StartDay, StartTimer);
        timer.OnTimeCompleted -= StopTimer;
    }
}
