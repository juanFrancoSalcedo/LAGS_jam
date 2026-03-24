using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandler : MonoBehaviour,IDialogListener
{
    [SerializeField] private TriggerDetector triggerDetector;
    [SerializeField] private List<DialogSheet> dialogs = new List<DialogSheet>();
    DialogManager dialogManager;

    public Action OnDialogComplete { get; set; }
    public Action OnDialogStarted { get; set; }
    public Action<int, int> OnDialogUpdate { get; set; }

    private void OnEnable()
    {
        triggerDetector.OnTriggerEntered += AllowTalk;
        triggerDetector.OnTriggerExited += DenyTalk;
        OnDialogComplete += InvokeCustomEvents;
    }

    private void InvokeCustomEvents()
    {
        print("Complete");
    }

    private void OnDisable()
    {
        triggerDetector.OnTriggerEntered -= AllowTalk;
        triggerDetector.OnTriggerExited -= DenyTalk;
        OnDialogComplete -= InvokeCustomEvents;
    }

    private void AllowTalk(Transform transform)
    {
        InteractIconService.Instance.ShowIcon();
        DialogManager.Instance.InjectDialogs(dialogs);
        DialogManager.Instance.InjectListener(this);
    }

    private void DenyTalk(Transform transform) => InteractIconService.Instance.HideIcon();

    private void Start() => dialogManager = DialogManager.Instance;
}



