using UnityEngine;

[System.Serializable]
public class EmployeeModel:ICopy<EmployeeModel>
{
    public TypeEmployeeTask typeTask;
    [Range(1,3)]
    public int levelJob;
    public string UID;
    public int Pricing;
    [TextArea(1,2)]
    public string DialogCV;

    public EmployeeModel Copy()
    {
        return (EmployeeModel)this.MemberwiseClone();
    }

    public void InitSettings()
    {
        if(string.IsNullOrEmpty(UID))
            UID = System.Guid.NewGuid().ToString();
    }
}
