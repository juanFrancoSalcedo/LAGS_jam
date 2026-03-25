using UnityEngine;

[System.Serializable]
public class DialogModel:ICopy<DialogModel>
{
    [TextArea(0,4)]
    public string Dialog;

    public DialogModel Copy()
    {
        return (DialogModel) this.MemberwiseClone();
    }
}