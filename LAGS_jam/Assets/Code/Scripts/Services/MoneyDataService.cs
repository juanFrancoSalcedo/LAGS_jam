using System;
using UnityEditor;
using UnityEngine;

public class MoneyDataService 
{
    public static event Action<int, int> OnMoneyChanged;

    public static void AddMoney(int plus) 
    {
        int before = 0;
        var buffer = before = PlayerPrefs.GetInt(KeyStorage.Money,0);
        buffer += plus;
        PlayerPrefs.SetInt(KeyStorage.Money, buffer);
        OnMoneyChanged?.Invoke(before,buffer);
    }

    public static void RemoveMoney(int less) 
    {
        int before = 0;
        var buffer = before = PlayerPrefs.GetInt(KeyStorage.Money, 0);
        buffer -= less;
        PlayerPrefs.SetInt(KeyStorage.Money, buffer);
        //OnMoneyChanged?.Invoke(before, buffer);
    }

    public static int GetMoney() => PlayerPrefs.GetInt(KeyStorage.Money, 0);
    
    public static bool CanPay(int money) => GetMoney() >= money;

#if UNITY_EDITOR
    [MenuItem("Tools/600")]
    private static void Quinientos() 
    {
        PlayerPrefs.SetInt(KeyStorage.Money, 600);
    }
#endif
}