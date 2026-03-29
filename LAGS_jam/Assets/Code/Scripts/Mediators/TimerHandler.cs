using UnityEngine;

public class TimerHandler:MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] private DialogSheet dialogSheetTickTackOne;
    [SerializeField] private DialogSheet dialogSheetTickTackTwo;

    bool showWarningOne;
    bool showWarningTwo;

    private void OnEnable()
    {
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.EnterCave, StartTimer);
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.OutCave, PauseTime);
        timer.OnTimeCompleted += StopTimer;
        timer.OnUpdateTime += CheckTimerEvents;
    }

    private void StartTimer() => timer.StartTimer();

    private void StopTimer() 
    {
        timer.StopTimer();
        GameStateContext.ChangeState(TypeGameState.EndDay);
        showWarningTwo = showWarningOne = false;
        AudioManager.Instance.StopTickTackOne();
        AudioManager.Instance.StopTickTackTwo();
    }

    private void PauseTime() 
    {
        timer.PauseTime();
    }

    private void OnDisable()
    {
        GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.EnterCave, StartTimer);
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.OutCave, PauseTime);
        timer.OnTimeCompleted -= StopTimer;
        timer.OnUpdateTime -= CheckTimerEvents;
    }

    private void CheckTimerEvents(string time) 
    {
        if (timer.TimeRemaing < 50f && !showWarningTwo)
        { 
            AudioManager.Instance.PlayTickTackTwo();
            NotificationManager.Instance.ShowNotification(dialogSheetTickTackTwo.Model.GetDialog(), 4f);
            showWarningTwo = true;
        }

        if (timer.TimeRemaing < 20f && !showWarningOne)
        { 
            AudioManager.Instance.PlayTickTackOne();
            NotificationManager.Instance.ShowNotification(dialogSheetTickTackOne.Model.GetDialog(), 4f);
            showWarningOne = true;
        }
    }
}
