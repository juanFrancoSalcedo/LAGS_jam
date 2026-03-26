using UnityEngine;

[CreateAssetMenu(fileName = "New Employes", menuName ="SO/Employee")]
public class EmployeeSheet : ScriptableObject
{
    [SerializeField] private Sprite spt;
    public Sprite Sprite => spt;
    [SerializeField] private EmployeeModel model;
    public EmployeeModel Model => model;

}
