using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TraderHandler : MonoBehaviour
{
    [SerializeField] private NPCHandler npcHandler;
    [SerializeField] private int eventIndex;
    [SerializeField] private int eventIndexAccept;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private GameObject panelTradeInvetory;
    [SerializeField] private GameObject panelAcceptTrade;
    [SerializeField] private ButtonNextDialog buttonDialog;

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
            panelTradeInvetory.SetActive(true);
            buttonDialog.SetInteract(false);   
        }

        if (arg1 == eventIndexAccept)
        { 
            panelAcceptTrade.SetActive(true);
            buttonDialog.SetInteract(false);
        }
    }
    
    ResourceModel bufferModel;
    private void Trade(ResourceModel model)
    {
        dialogManager.AddCustomDialog($"Te doy {model.Pricing:F0} monedas por tu {model.Name}");
        panelTradeInvetory.SetActive(false);
        bufferModel = model.Copy();
        //print($"Te doy {model.Pricing:F0} monedas por tu {model.Name} ({model.typeResource} - {model.Quality})");
    }

    public void AcceptTrade(bool accept) 
    {
        if (accept)
        {
            InventoryDataService.RemoveItem(bufferModel);
            MoneyDataService.AddMoney(bufferModel.Pricing);
            dialogManager.AddCustomDialog("Perfecto tome su plata");
        }
        else
        { 
            dialogManager.AddCustomDialog("Está bien, para otra ocación");
        }
        panelAcceptTrade?.SetActive(false);
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
