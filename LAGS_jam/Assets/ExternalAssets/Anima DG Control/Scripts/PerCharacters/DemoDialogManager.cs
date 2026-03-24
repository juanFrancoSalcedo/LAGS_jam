using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;
using B_Extensions;
using UnityEngine.Events;

public class DemoDialogManager:MonoBehaviour
{
    [SerializeField] DemoDialogAnimation dialogDisplayer;
    [SerializeField] public List<DialogSheet> dialogs = new List<DialogSheet>();
    [SerializeField] List<string> interfaces = new List<string>();

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

    private UnityEvent OnCompleted;

    private int indexDialog;
    bool completeCurrentDailog;

    public void TryDialog() 
    {
        if()
    }

    private IEnumerator DoDialog()
    {
        yield return new WaitForSeconds(0.1f);
#if ANIMA_DOTWEEN_PRO
        Next();
#else
        print("You don't have DOTween Pro. This script will not work.");
#endif
    }

#if ANIMA_DOTWEEN_PRO
    public void Next() 
    {
        // here get the type of the class by it name, the first of the list
        Type tipo = Type.GetType(interfaces[0]);

        if (tipo != null)
        {
            object instancia = Activator.CreateInstance(tipo);
            if (instancia is ITypingAnimaStrategy asInterface)
            {
                dialogDisplayer.AnimateText(asInterface, dialogs[indexDialog].Model.Dialog);
            }
        }
    }
#endif
}



public class ButtonNextDialog : BaseButtonAttendant
{

    InputSystem_Actions actions;

    private void Awake()
    {
        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Update()
    {
        if(actions.FindAction("Jump").WasPressedThisFrame())
            NextDialog();

    }

    private void Start()
    {
        buttonComponent.onClick.AddListener(NextDialog);    
    }

    private void NextDialog()
    {
        
    }
}