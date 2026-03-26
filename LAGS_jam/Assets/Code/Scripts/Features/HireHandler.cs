using System;
using System.Linq;
using UnityEngine;

public class HireHandler : MonoBehaviour, IDialogListener
{
    [SerializeField] private NPCHandler npcHandler;
    [SerializeField] private int eventIndexAccept;
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private GameObject panelAcceptHire;
    [SerializeField] private ButtonNextDialog buttonDialog;
    [SerializeField] private EmployeeSheet employeerSheet;
    [SerializeField] private ButtonAcceptHire[] buttonHires;

    public Action OnDialogStarted { get; set; }
    public Action<int, int> OnDialogUpdate { get; set; }
    public Action OnDialogComplete { get; set; }
    private void Start()
    {
        if (HireDataService.runTimeData != null)
        {
            if(InDataBase)
                gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        npcHandler.OnDialogStarted += InsertInfoInDialog;
        npcHandler.OnDialogUpdate += ListenEventIndex;
        npcHandler.OnDialogComplete += ListenComplete;
    }
    private void OnDisable()
    {
        npcHandler.OnDialogStarted -= InsertInfoInDialog;
        npcHandler.OnDialogUpdate -= ListenEventIndex;
        npcHandler.OnDialogComplete -= ListenComplete;
    }

    private void InsertInfoInDialog()
    {

        if (InDataBase)
            return;
        dialogManager.AddCustomDialog(employeerSheet.Model.DialogCV);
        if (buttonHires.Length == 0)
            print("HAY QUE PONER LOS BOTONES VIEJO");
        foreach (var item  in buttonHires) 
        {
            item.npcHandler = this;
        }
    }

    public bool InDataBase => HireDataService.runTimeData.employees.Any(e => e.UID == employeerSheet.Model.UID);


    private void ListenComplete() => Invoke(nameof(CheckInvetory), 0.1f);

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
            if (MoneyDataService.CanPay(employeerSheet.Model.Pricing))
            { 
                HireDataService.AddItem(employeerSheet.Model);
                MoneyDataService.RemoveMoney(employeerSheet.Model.Pricing);
                dialogManager.AddCustomDialog("Perfecto trabajaré duro");
            }
            else
                dialogManager.AddCustomDialog("Lo siento, no tienes dinero suficiente para pagarme");
        }
        else
        {
            dialogManager.AddCustomDialog("Tu te lo pierdes");
        }
        panelAcceptHire?.SetActive(false);
    }
}

