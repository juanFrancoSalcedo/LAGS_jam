using B_Extensions;
using System;
using TMPro;
using UnityEngine;
using Zenject;

public class ButtonSellAll : BaseButtonAttendant
{
    [SerializeField] TMP_Text textInner;
    [SerializeField] private DialogSheet dialogSheet;
    [Inject] DialogManager dialogManager;
    void Start()
    {
        buttonComponent.onClick.AddListener(SellAll);
        var total =  CalculateTotalPrice();
        textInner.text = dialogSheet.Model.GetDialog().Replace("#",total.ToString());
    }

    private void SellAll()
    {
        MoneyDataService.AddMoney(CalculateTotalPrice());
        InventoryDataService.RemoveAll();
        dialogManager.ReleaseChat();
    }

    private int CalculateTotalPrice() 
    {
        int total = 0;
        InventoryDataService.runTimeData.resources.ForEach(x => 
        {
            total += x.Pricing;
            total -= 2;
        });
        return total;
    }
}
