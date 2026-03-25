using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dialog", fileName = "New Dialog")]
public class DialogSheet : ScriptableObject 
{
    [SerializeField] private DialogModel model;
    public DialogModel Model => model;
}
