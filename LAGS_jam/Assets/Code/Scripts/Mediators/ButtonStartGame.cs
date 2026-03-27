using B_Extensions;
using System.Collections;
using TMPro;
using UnityEngine;

public class ButtonStartGame:BaseButtonAttendant
{
    [SerializeField] private AnimationUIController animaUI;
    [SerializeField] private TMP_Text textComponent;
    private void Start()
    {
        buttonComponent.onClick.AddListener(StartGame);
        StartCoroutine(ReadText());
    }

    private IEnumerator ReadText() 
    {
        while (true)
        {
            textComponent.text =
                PlayerPrefs.HasKey(KeyStorage.Day)?"Continuar":"Juego Nuevo";
            yield return new WaitForSeconds(1f);
        }
    }
    
    private void StartGame()
    {
        if (PlayerPrefs.HasKey(KeyStorage.Presentation_1))
        {
            GameStateContext.ChangeState(TypeGameState.StartDay);
        }
        else
        {
            PlayerPrefs.SetInt(KeyStorage.Presentation_1, 1);
            animaUI.ActiveAnimation();
            animaUI.gameObject.SetActive(true);
        }
    }
}