using B_Extensions;

public class ButtonStartGame:BaseButtonAttendant
{
    private void Start()
    {
        buttonComponent.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        GameStateMediator.Publish(TypeGameState.StartDay);
    }
}