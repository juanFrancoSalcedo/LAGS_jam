using UnityEngine;

public class TimerHandler:MonoBehaviour
{
    [SerializeField] Timer timer;

    private void OnEnable()
    {
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.StartDay, StartTimer);
        timer.OnTimeCompleted += StopTimer;
        timer.OnUpdateTime += CheckTimerEvents;
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
        timer.OnUpdateTime -= CheckTimerEvents;
    }

    private void CheckTimerEvents(string time) 
    {
        if (timer.TimeRemaing < 50f)
            AudioManager.Instance.PlayTickTackTwo();

        if (timer.TimeRemaing < 20f)
            AudioManager.Instance.PlayTickTackOne();
    }
}
