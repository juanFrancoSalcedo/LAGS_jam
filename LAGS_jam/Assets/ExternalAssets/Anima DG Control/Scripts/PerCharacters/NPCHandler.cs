using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCHandler : MonoBehaviour,IDialogListener
{
    [SerializeField] private TriggerDetector triggerDetector;
    [SerializeField] private List<DialogSheet> dialogs = new List<DialogSheet>();
    [SerializeField] private UnityEvent OnCompleteDialog;
    DialogManager dialogManager;

    public Action OnDialogComplete { get; set; }
    public Action OnDialogStarted { get; set; }
    public Action<int, int> OnDialogUpdate { get; set; }

    private void OnEnable()
    {
        triggerDetector.OnTriggerEntered += AllowTalk;
        triggerDetector.OnTriggerExited += DenyTalk;
        OnDialogComplete += InvokeCustomEvents;
        if(GameStateContext.State != TypeGameState.StartDay)
            ActiveDeco(false);
        GameStateContext.GameStateMediator.Subscribe(TypeGameState.StartDay,()=>ActiveDeco(true));
    }


    private void OnDisable()
    {
        triggerDetector.OnTriggerEntered -= AllowTalk;
        triggerDetector.OnTriggerExited -= DenyTalk;
        OnDialogComplete -= InvokeCustomEvents;
        GameStateContext.GameStateMediator.Unsubscribe(TypeGameState.StartDay, () => ActiveDeco(true));
    }

    private void ActiveDeco(bool value)
    {
        for (int i = 0;i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }

    private void InvokeCustomEvents() => OnCompleteDialog?.Invoke();
    public void AllowTalk(Transform transform)
    {
        InteractIconService.Instance.ShowIcon();
        var dialogSend = new List<DialogModel>();
        dialogs.ForEach(t =>dialogSend.Add(t.Model.Copy()));
        DialogManager.Instance.InjectDialogs(dialogSend);
        DialogManager.Instance.InjectListener(this);
    }

    private void DenyTalk(Transform transform)
    {
        InteractIconService.Instance.HideIcon();
        DialogManager.Instance.ReleaseChat();
    }

    private void Start() => dialogManager = DialogManager.Instance;
}