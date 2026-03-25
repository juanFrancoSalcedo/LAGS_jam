using System;
using System.Linq;
using UnityEngine;

public class InventoryDataService
{
    public static ResourceWrapper runTimeData = new ResourceWrapper();

    public static void SaveData() 
    {
        var json = JsonUtility.ToJson(runTimeData);
        PlayerPrefs.SetString(KeyStorage.Inventory_Resources,json);
    }

    public static ResourceWrapper ReadData() 
    {
        runTimeData = JsonUtility.FromJson<ResourceWrapper>(PlayerPrefs.GetString(KeyStorage.Inventory_Resources));
        return runTimeData;
    }

    public static void AddItem(ResourceModel model)
    {
        if (string.IsNullOrEmpty(model.UID))
            model.InitSettings();
        runTimeData.resources.Add(model);
        SaveData();
    }

    public static void RemoveItem(ResourceModel model)
    {
        if (string.IsNullOrEmpty(model.UID))
        { 
            Debug.LogError("EMPTY UID");
            return;
        }

        var item = runTimeData.resources.First(i => i.UID.Equals(model.UID));

        if (item != null)
        { 
            runTimeData.resources.Remove(item);
            SaveData();
        }
    }
}