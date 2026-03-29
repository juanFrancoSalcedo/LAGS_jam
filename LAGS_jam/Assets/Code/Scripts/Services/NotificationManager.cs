using B_Extensions;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NotificationManager : Singleton<NotificationManager>
{
    [SerializeField] AnimationUIController animationUI;
    [SerializeField] DialogAnimation dialogAnimation;

    private Queue<NotificationData> notificationQueue = new Queue<NotificationData>();
    
    private bool isShowingNotification = false;

    protected override void Awake()
    {
        base.Awake();
        // Suscribirse al evento de finalización de la animación
        if (animationUI != null)
        {
            animationUI.OnEndedCallBack.AddListener(OnNotificationEnded);
        }
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento
        if (animationUI != null)
        {
            animationUI.OnEndedCallBack.RemoveListener(OnNotificationEnded);
        }
    }

    public void ShowNotification(string message, float timeReading)
    {
        NotificationData notification = new NotificationData
        {
            message = message,
            timeReading = timeReading + 0.9f//Delay betwen notification
        };

        // Añadir a la cola
        notificationQueue.Enqueue(notification);
        ProcessNextNotification();
    }


    private async void ProcessNextNotification()
    {
        await UniTask.Delay(300);
        if (notificationQueue.Count == 0)
        {
            isShowingNotification = false;
            return;
        }

        NotificationData notification = notificationQueue.Dequeue();
        DisplayNotification(notification);
    }

    private async void DisplayNotification(NotificationData notification)
    {
        if (isShowingNotification)
            return;
        isShowingNotification = true;
        animationUI.ActiveAnimation(0);
        dialogAnimation.AnimateDefault(notification.message);
        
        if (animationUI.listAux != null && animationUI.listAux.Count > 1)
        {
            animationUI.listAux[1].delay = notification.timeReading;
            await UniTask.Delay((int)(notification.timeReading * 1000)); 
        }
        isShowingNotification = false;
    }

    private void OnNotificationEnded()
    {
        dialogAnimation.ClearText();
        ProcessNextNotification();
    }

    public void ClearAllNotifications()
    {
        notificationQueue.Clear();
        isShowingNotification = false;
        dialogAnimation.ClearText();
    }

    public int GetPendingNotificationsCount() => notificationQueue.Count;

    private class NotificationData
    {
        public string message;
        public float timeReading;
    }
}
