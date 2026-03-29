using System;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TMP_Text))]
public class TextMoney : MonoBehaviour
{
    [SerializeField] string label = "Dinero: @";
    TMP_Text textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        OnMoneyChanged(0, MoneyDataService.GetMoney());
    }

    private void OnEnable() => MoneyDataService.OnMoneyChanged += OnMoneyChanged;
    private void OnDisable() => MoneyDataService.OnMoneyChanged -= OnMoneyChanged;

    private void OnMoneyChanged(int arg1, int arg2)
    {
        Invoke(nameof(ShowMoney),2f);
    }

    private void ShowMoney() 
    {
        textComponent.text = label.Replace("@", MoneyDataService.GetMoney().ToString());
    }
}
