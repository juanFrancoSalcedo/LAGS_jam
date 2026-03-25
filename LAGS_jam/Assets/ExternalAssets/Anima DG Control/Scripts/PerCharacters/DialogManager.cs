using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Threading;
using B_Extensions;


public class DialogManager:Singleton<DialogManager>
{
    [SerializeField] DialogAnimation dialogDisplayer;
    [SerializeField] GameObject container = null;
    [SerializeField] List<string> interfaces = new List<string>();
    public List<DialogModel> dialogs = new List<DialogModel>();

    public int IndexDialog { get; set; }
    private bool animatingDialog = false;

    private void OnValidate()
    {
#if ANIMA_DOTWEEN_PRO
        var interfaceType = typeof(ITypingAnimaStrategy);
        var implementingTypes = Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass);
        foreach (var type in implementingTypes)
        {
            if (!interfaces.Contains(type.Name))
            {
                interfaces.Add(type.Name);
            }
        }
#endif
    }

    List<IDialogListener> dialogsListener = new List<IDialogListener>();

    public void InjectDialogs(List<DialogModel> sheets) 
    {
        dialogs.Clear();
        dialogs.AddRange(sheets);
    }

    public void InjectListener(IDialogListener diaologListener) => dialogsListener.Add(diaologListener);

    public async void AddCustomDialog(string dialog) 
    {
        DialogModel dialogModel = new DialogModel();
        dialogModel.Dialog = dialog;
        dialogs.Add(dialogModel);
        await Next();
    }

    public async void TryDialog() 
    {
        if (!container.activeInHierarchy)
        {
            dialogDisplayer.ClearText();
            container.SetActive(true);
            Invoke(nameof(TryDialog),0.1f);
            dialogsListener.ForEach(d => d.OnDialogStarted?.Invoke());
            return;
        }
        else if (!animatingDialog)
        {
            await Next();
        }
    }


#if ANIMA_DOTWEEN_PRO
    private async UniTask Next()
    {
        if (CheckCompleted())
        {
            container.SetActive(false);
            return;
        }
        else
            container.SetActive(true);

        await AnimationDialog();

        dialogsListener.ForEach(d => d.OnDialogUpdate?.Invoke(IndexDialog, dialogs.Count));
        IndexDialog++;

    }

    private async UniTask AnimationDialog()
    {
        animatingDialog = true;
        // here get the type of the class by it name, the first of the list
        Type tipo = Type.GetType(interfaces[0]);

        if (tipo != null)
        {
            object instancia = Activator.CreateInstance(tipo);
            if (instancia is ITypingAnimaStrategy asInterface)
                await dialogDisplayer.AnimateText(asInterface, dialogs[IndexDialog].Dialog);
        }
        animatingDialog = false;
    }

    private bool CheckCompleted()
    {
        if (IndexDialog >= dialogs.Count)
        {
            IndexDialog = 0;
            if (dialogsListener.Count > 0)
            { 
                dialogsListener.ForEach(dialog => dialog.OnDialogComplete?.Invoke());
                print("TODO Change this to UnityEvent to wrap UnityActions");
                dialogDisplayer.ClearText();
                dialogs.Clear();
            }
            return true;
        }
        return false;
    }
#endif
}

[System.Serializable]
public class Icon 
{
    public Sprite sprite;
    public string TypeInTheFuture;
}
