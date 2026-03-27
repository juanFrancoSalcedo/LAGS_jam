using System;
using UnityEngine;

public static class LanguageService
{
    public static event Action<TypeLanguage> OnLanguageChanged;

    public static void ChangeLanguage(TypeLanguage language)
    {
        PlayerPrefs.SetString(KeyStorage.Language, language.ToString());
        KeyStorage.Constants.CurrentLanguage = language;
        OnLanguageChanged?.Invoke(language);
    }


    public static TypeLanguage StartLanguage()
    {
        string languageString = PlayerPrefs.GetString(KeyStorage.Language, TypeLanguage.Spanish.ToString());
        
        // Convertir string a enum
        if (Enum.TryParse<TypeLanguage>(languageString, out TypeLanguage result))
        {
            KeyStorage.Constants.CurrentLanguage = result;
            return result;
        }
        
        // Si falla, retornar el idioma por defecto
        KeyStorage.Constants.CurrentLanguage = TypeLanguage.Spanish;
        return TypeLanguage.Spanish;
    }
}