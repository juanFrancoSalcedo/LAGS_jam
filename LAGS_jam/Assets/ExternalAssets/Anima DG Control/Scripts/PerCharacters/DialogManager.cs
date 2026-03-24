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
    [SerializeField] DemoDialogAnimation dialogDisplayer;
    [SerializeField] GameObject container = null;
    [SerializeField] List<string> interfaces = new List<string>();
    public List<DialogSheet> dialogs = new List<DialogSheet>();

    private int indexDialog;
    bool animatingDialog = false;

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

    public void InjectDialogs(List<DialogSheet> sheets) 
    {
        dialogs.Clear();
        dialogs.AddRange(sheets);
    }

    public void InjectListener(IDialogListener diaologListener) => dialogsListener.Add(diaologListener);


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

        animatingDialog = true;
        // here get the type of the class by it name, the first of the list
        Type tipo = Type.GetType(interfaces[0]);

        if (tipo != null)
        {
            object instancia = Activator.CreateInstance(tipo);
            if (instancia is ITypingAnimaStrategy asInterface)
                await dialogDisplayer.AnimateText(asInterface, dialogs[indexDialog].Model.Dialog);
        }

        dialogsListener.ForEach(d => d.OnDialogUpdate?.Invoke(indexDialog, dialogs.Count));

        animatingDialog = false;
        indexDialog++;
        
    }

    private bool CheckCompleted()
    {
        if (indexDialog >= dialogs.Count)
        {
            indexDialog = 0;
            if (dialogsListener.Count > 0)
            { 
                dialogsListener.ForEach(dialog => dialog.OnDialogComplete?.Invoke());
                print("TODO Change this to UnityEvent to wrap UnityActions");
                dialogDisplayer.ClearText();
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
