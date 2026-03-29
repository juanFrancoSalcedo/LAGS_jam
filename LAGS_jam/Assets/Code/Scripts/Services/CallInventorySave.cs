using UnityEngine;

public class CallInventorySave : MonoBehaviour
{
    public void Start()
    {
        
    }

    public void SaveInventory() 
    {
        InventoryDataService.SaveData();
    }
}