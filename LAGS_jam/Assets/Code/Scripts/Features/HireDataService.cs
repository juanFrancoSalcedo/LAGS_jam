using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class HireDataService
{
    public static EmployeeWrapper runTimeData = new EmployeeWrapper();

    public static void SaveData()
    {
        var json = JsonUtility.ToJson(runTimeData);
        PlayerPrefs.SetString(KeyStorage.Employees_Hired, json);
    }

    public static EmployeeWrapper ReadData()
    {
        if(PlayerPrefs.HasKey(KeyStorage.Employees_Hired))
            runTimeData = JsonUtility.FromJson<EmployeeWrapper>(PlayerPrefs.GetString(KeyStorage.Employees_Hired));
        return runTimeData;
    }

    public static void AddItem(EmployeeModel model)
    {
        if (string.IsNullOrEmpty(model.UID))
            model.InitSettings();
        Debug.Log(runTimeData == null);
        Debug.Log(runTimeData.employees == null);
        Debug.Log(model == null);
        runTimeData.employees.Add(model);
        SaveData();
    }

    public static void RemoveItem(EmployeeModel model)
    {
        if (string.IsNullOrEmpty(model.UID))
        {
            Debug.LogError("EMPTY UID");
            return;
        }

        var item = runTimeData.employees.First(i => i.UID.Equals(model.UID));

        if (item != null)
        {
            runTimeData.employees.Remove(item);
            SaveData();
        }
    }
}
