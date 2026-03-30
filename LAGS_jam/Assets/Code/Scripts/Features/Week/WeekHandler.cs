using TMPro;
using UnityEngine;

public class WeekHandler : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text passGameText;
    [SerializeField] private TMP_Text dontPassGameText;
    [SerializeField] private ButtonStartGame buttonStartGame;
    private void OnEnable()
    {       
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.FinishGame,OpenWeekPanel);
    }
    private void OnDisable()
    {
        GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.FinishGame, OpenWeekPanel);
    }

    private void OpenWeekPanel()
    {
        panel.gameObject.SetActive(true);
        if(MoneyDataService.Money>=500)
            passGameText.gameObject.SetActive(true);
        else
            dontPassGameText.gameObject.SetActive(true);
        PlayerPrefs.DeleteAll();
        buttonStartGame.ResetUI();
    }
}
