using UnityEngine;

public class MoneyDataService 
{
    public static void AddMoney(int plus) 
    {
        var buffer = PlayerPrefs.GetInt(KeyStorage.Money,0);
        buffer+= plus;
        PlayerPrefs.SetInt(KeyStorage.Money, buffer);
    }

    public static void RemoveMoney(int less) 
    {

        var buffer = PlayerPrefs.GetInt(KeyStorage.Money, 0);
        buffer -= less;
        PlayerPrefs.SetInt(KeyStorage.Money, buffer);
    }

    public static int GetMoney() 
    {
        return PlayerPrefs.GetInt(KeyStorage.Money,0);
    }

}