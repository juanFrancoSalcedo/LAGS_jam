using UnityEngine;
using B_Extensions;

public class InteractIconService : Singleton<InteractIconService> 
{
    [SerializeField] private GameObject icon;
    public bool ShowingIcon { get; private set; } = false;
    public event System.Action<bool> OnIconStateChanged;
    public void ShowIcon()
    {
        icon.SetActive(true);
        ShowingIcon = true;
        OnIconStateChanged?.Invoke(ShowingIcon);
    }

    public void HideIcon()
    {
        icon.SetActive(false);
        ShowingIcon = false;
        OnIconStateChanged?.Invoke(ShowingIcon);
    }
}



