using B_Extensions;
using UnityEngine;

public class NotificationService : Singleton<NotificationService>
{
    [SerializeField] AnimationUIController animationUI;
    public void ShowNotification(string message,float timeReading)
    {
        animationUI.ActiveAnimation(0);
        animationUI.listAux[1].delay = timeReading;
    }
}
