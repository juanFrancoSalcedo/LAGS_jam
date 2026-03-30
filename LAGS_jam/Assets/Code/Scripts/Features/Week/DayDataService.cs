using UnityEngine;

public static class DayDataService 
{
    static int currentDay = 1;
    public static event System.Action<int,int> OnDayChanged;

    public static bool IsLastDay() => currentDay >= 4;

    public static int GetCurrentDay()
    {
        return currentDay;
    }

    public static void AddDay()
    {
        currentDay = PlayerPrefs.GetInt(KeyStorage.Day, 1);
        var buffer = currentDay;
        currentDay++;
        OnDayChanged?.Invoke(buffer,currentDay);
        SaveData();
    }

    public static void ResetWeek() 
    {
        currentDay = 1;
        SaveData();
    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt(KeyStorage.Day, currentDay);
    }
}

