using UnityEngine;

public class CallerNotification : MonoBehaviour
{
    [SerializeField] DialogSheet dialog;
    NotificationManager notifyManager;

    private void Start()
    {
        notifyManager  = NotificationManager.Instance;
    }
    public void CallNotification() 
    {
        notifyManager.ShowNotification(dialog.Model.GetDialog(),dialog.TimeReading);
    }
}
