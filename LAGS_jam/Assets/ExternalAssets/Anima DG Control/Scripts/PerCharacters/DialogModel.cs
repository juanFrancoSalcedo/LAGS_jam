using B_Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class DialogModel : ICopy<DialogModel>
{
    [TextArea(0, 4)]
    [SerializeField] private string Dialog;
    [TextArea(0, 4)]
    [SerializeField] private string DialogEN;
    [TextArea(0, 4)]
    [SerializeField] private string DialogPO;
    [SerializeField] private bool useWildCards;


    public DialogModel SetWildCards(Dictionary<string,string> dictoWildcards) 
    {

        switch (KeyStorage.Constants.CurrentLanguage)
        {
            case TypeLanguage.None:
                break;
            case TypeLanguage.Spanish:
                foreach (var item in dictoWildcards)
                {
                    Dialog = Dialog.Replace(item.Key, item.Value);
                }
                break;
            case TypeLanguage.English:
                foreach (var item in dictoWildcards)
                {
                    DialogEN = DialogEN.Replace(item.Key, item.Value);
                }
                break;
            case TypeLanguage.Portuguese:
                foreach (var item in dictoWildcards)
                {
                    DialogPO = DialogPO.Replace(item.Key, item.Value);
                }
                break;
            default:
                break;
        }
        return this;
    }


    public string GetDialog()
    {
        switch (KeyStorage.Constants.CurrentLanguage)
        {
            case TypeLanguage.None:
                return Dialog;
            case TypeLanguage.Spanish:
                return Dialog;
            case TypeLanguage.English:
                return DialogEN;
            case TypeLanguage.Portuguese:
                return DialogPO;
            default:
                return Dialog;
        }
    }
    public DialogModel Copy()
    {
        return (DialogModel) this.MemberwiseClone();
    }
}


public class ButtonLanguage : BaseButtonAttendant
{
    [SerializeField] TypeLanguage language;
    private void Start()
    {
        buttonComponent.onClick.AddListener(SelectLanguage);
    }

    private void SelectLanguage()
    {
        KeyStorage.Constants.CurrentLanguage = language;
    }
}
