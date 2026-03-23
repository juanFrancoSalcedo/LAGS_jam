using System;
using UnityEngine;

public class InventoryService : MonoBehaviour
{
    private void OnEnable()
    {
        InventoryMediator.Subscribe(TypeResource.EmeraldCristals,Collect);    
    }

    private void OnDisable() 
    {
        InventoryMediator.Unsubscribe(TypeResource.EmeraldCristals, Collect);
    }

    private void Collect()
    {
        var c = PlayerPrefs.GetInt(KeyStorage.Count_emerald,0);
        PlayerPrefs.SetInt(KeyStorage.Count_emerald,c);
    }
}
