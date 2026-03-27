using TMPro;
using UnityEngine;

public class WeekHandler : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text passGameText;
    [SerializeField] private TMP_Text dontPassGameText;
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
        if(MoneyDataService.GetMoney()>=500)
            passGameText.gameObject.SetActive(true);
        else
            dontPassGameText.gameObject.SetActive(true);
        PlayerPrefs.DeleteAll();
    }
}
