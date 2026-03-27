using B_Extensions;
using UnityEngine;

public class ButtonLanguage : BaseButtonAttendant
{
    [SerializeField] TypeLanguage language;
    private void Start()
    {
        buttonComponent.onClick.AddListener(SelectLanguage);
    }

    private void SelectLanguage()
    {
        LanguageService.ChangeLanguage(language);
    }
}
