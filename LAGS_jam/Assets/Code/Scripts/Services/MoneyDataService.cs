using System;
using UnityEditor;
using UnityEngine;

public class MoneyDataService 
{
    public static event Action<int, int> OnMoneyChanged;

    private static int money;
    public static int Money => money;

    public static void AddMoney(int plus) 
    {
        int before = 0;
        var buffer = before = money;
        money += plus;
        OnMoneyChanged?.Invoke(before,buffer);
        SaveData();
    }


    public static void SaveData() => PlayerPrefs.SetInt(KeyStorage.Money, money);
    public static void RemoveMoney(int less) 
    {
        money -= less;
        PlayerPrefs.SetInt(KeyStorage.Money, money);
        //OnMoneyChanged?.Invoke(before, buffer);
    }

    public static int FirstGet()
    {
        money = PlayerPrefs.GetInt(KeyStorage.Money, 0);
        return money;
    }

    public static bool CanPay(int _money) => money >= _money;

#if UNITY_EDITOR
    [MenuItem("Tools/600")]
    private static void Quinientos() 
    {
        PlayerPrefs.SetInt(KeyStorage.Money, 600);
    }
#endif
}