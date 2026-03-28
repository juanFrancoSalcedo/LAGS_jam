using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dialog", fileName = "New Dialog")]
public class DialogSheet : ScriptableObject 
{
    [SerializeField] private float timeReading = 6f;
    public float TimeReading => timeReading;
    [SerializeField] private DialogModel model;
    public DialogModel Model => model;
}
