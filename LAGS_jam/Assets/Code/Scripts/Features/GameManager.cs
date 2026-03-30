
using UnityEngine;
using B_Extensions;

public class GameManager : Singleton<GameManager>
{
    private new void Awake()
    {
        base.Awake();
        LanguageService.StartLanguage();
        MoneyDataService.FirstGet();
    }
}
