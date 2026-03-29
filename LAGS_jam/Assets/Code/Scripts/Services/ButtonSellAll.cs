using B_Extensions;
using System;
using TMPro;
using UnityEngine;

public class ButtonSellAll : BaseButtonAttendant
{
    [SerializeField] TMP_Text textInner;
    [SerializeField] private DialogSheet dialogSheet;

    void Start()
    {
        buttonComponent.onClick.AddListener(SellAll);
        var total =  CalculateTotalPrice();
        textInner.text = dialogSheet.Model.GetDialog().Replace("#",total.ToString());
    }

    private void SellAll()
    {
        InventoryDataService.RemoveAll();
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
