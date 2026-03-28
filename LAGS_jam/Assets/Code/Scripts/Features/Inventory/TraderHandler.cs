using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

public class TraderHandler : MonoBehaviour
{
    [SerializeField] private NPCHandler npcHandler;
    [SerializeField] private int eventIndex;
    [SerializeField] private int eventIndexAccept;
    [Header("Dialogs")]
    [SerializeField] private DialogSheet dialogModelSale;
    [SerializeField] private DialogSheet dialogAcceptTrade;
    [SerializeField] private DialogSheet dialogDeclineTrade;
    [Inject] private DialogManager dialogManager;

    private void Awake()
    {
        InventoryDataService.ReadData();
    }

    private void OnEnable()
    {
        CardInventoryItem.OnTryTrade += Trade;
        npcHandler.OnDialogUpdate += ListenEventIndex;
        npcHandler.OnDialogComplete += ListenComplete;
    }
    private void OnDisable()
    {
        CardInventoryItem.OnTryTrade -= Trade;
        npcHandler.OnDialogUpdate -= ListenEventIndex;
        npcHandler.OnDialogComplete -= ListenComplete;
    }

    private void ListenEventIndex(int arg1, int arg2)
    {
        if (arg1 == eventIndex)
        {
            dialogManager.PanelInventory.SetActive(true);
            dialogManager.ButtonDialog.SetInteract(false);   
        }

        if (arg1 == eventIndexAccept)
        {
            dialogManager.PanelAcceptTrade.SetActive(true);
            dialogManager.ButtonDialog.SetInteract(false);
        }
    }
    
    ResourceModel bufferModel;
    private void Trade(ResourceModel model)
    {
        var wildCards = new Dictionary<string, string>
        {
            { "@", model.Pricing.ToString() },
            { "#", model.Name }
        };
        dialogManager.AddCustomDialog(dialogModelSale.Model.Copy().SetWildCards(wildCards));
        dialogManager.PanelInventory.SetActive(false);
        bufferModel = model.Copy();
    }

    public void AcceptTrade(bool accept) 
    {
        if (accept)
        {
            InventoryDataService.RemoveItem(bufferModel);
            MoneyDataService.AddMoney(bufferModel.Pricing);
            dialogManager.AddCustomDialog(dialogAcceptTrade.Model.Copy());
        }
        else
        { 
            dialogManager.AddCustomDialog(dialogDeclineTrade.Model.Copy());
        }
        dialogManager.PanelAcceptTrade?.SetActive(false);
    }

    private void ListenComplete()
    {
        Invoke(nameof(CheckInvetory),0.1f);
    }

    private void CheckInvetory()
    {
        if (InventoryDataService.runTimeData.resources.Count > 0)
            npcHandler.AllowTalk(null);
    }
}
