using B_Extensions;
using UnityEngine;

public class ButtonStartGame:BaseButtonAttendant
{
    [SerializeField] private AnimationUIController animaUI;
    private void Start()
    {
        buttonComponent.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        if (PlayerPrefs.HasKey(KeyStorage.Presentation_1))
        {
            GameStateMediator.Publish(TypeGameState.StartDay);
        }
        else
        {
            PlayerPrefs.SetInt(KeyStorage.Presentation_1, 1);
            animaUI.ActiveAnimation();
            animaUI.gameObject.SetActive(true);
        }
    }
}