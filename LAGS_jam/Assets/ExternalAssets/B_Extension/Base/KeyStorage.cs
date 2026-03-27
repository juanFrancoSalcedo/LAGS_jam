using System.Collections;

public static class KeyStorage
{
    public readonly static string Count_emerald = "KEY_EMERALD";
    public readonly static string Inventory_Resources = "INVENTORY_RESOURCES";
    public readonly static string Employees_Hired = "Employees_Hired";
    public readonly static string Money = "MONEY_I";

    public readonly static string Presentation_1 = "PRESENTATION_1_I";

    public static class Constants 
    {
        public static TypeLanguage CurrentLanguage = TypeLanguage.Spanish;
    }
}


public enum TypeLanguage 
{
    None,
    Spanish,
    English,
    Portuguese
}
