using System;
using System.Linq;
using UnityEngine;

public class HireHandler : MonoBehaviour
{
    [SerializeField] private NPCHandler npcHandler;
    [SerializeField] private int eventIndexAccept;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private GameObject panelAcceptHire;
    [SerializeField] private ButtonNextDialog buttonDialog;
    [SerializeField] private EmployeeSheet employeerSheet;

    private void Start()
    {
        if (HireDataService.runTimeData != null)
        {
            if(HireDataService.runTimeData.employees.Any(e => e.UID ==employeerSheet.Model.UID))
                gameObject.SetActive(false);
        }
    }


    private void OnEnable()
    {
        npcHandler.OnDialogStarted += InsertInfoInDialog;
        npcHandler.OnDialogUpdate += ListenEventIndex;
        npcHandler.OnDialogComplete += ListenComplete;
    }

    private void InsertInfoInDialog()
    {
        dialogManager.AddCustomDialog(employeerSheet.Model.DialogCV);
    }


    private void OnDisable()
    {
        npcHandler.OnDialogStarted -= InsertInfoInDialog;
        npcHandler.OnDialogUpdate -= ListenEventIndex;
        npcHandler.OnDialogComplete -= ListenComplete;
    }

    private void ListenComplete()
    {
        Invoke(nameof(CheckInvetory), 0.1f);
    }

    private void CheckInvetory()
    {
        if (InventoryDataService.runTimeData.resources.Count > 0)
            npcHandler.AllowTalk(null);
    }

    private void ListenEventIndex(int arg1, int arg2)
    {
        if (arg1 == eventIndexAccept)
        {
            panelAcceptHire.SetActive(true);
            buttonDialog.SetInteract(false);
        }
    }

    public void AcceptHire(bool accept)
    {
        if (accept)
        {
            HireDataService.AddItem(employeerSheet.Model);
            MoneyDataService.RemoveMoney(employeerSheet.Model.Pricing);
            dialogManager.AddCustomDialog("Perfecto dame dinero y trabajare duro");
        }
        else
        {
            dialogManager.AddCustomDialog("Tu te lo pierdes");
        }
        panelAcceptHire?.SetActive(false);
    }
}

