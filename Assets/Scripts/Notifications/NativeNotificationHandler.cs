using UnityEngine;

public class NativeNotificationHandler : MonoBehaviour
{
    [Header("Notiffication Preset")]
    [SerializeField] private NotificationPreset notificationPreset;

    [Header("Handlers")]
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] private IOSNotificationHandler iosNotificationHandler;

    private readonly int _notificationFireTime = 15;

    private void Awake()
    {
 #if UNITY_ANDROID
        androidNotificationHandler.RequestAuthorization();
        androidNotificationHandler.RegisterNotificationChannel();

        androidNotificationHandler.SendNotification(
            notificationPreset.GetRandomNotificationTitle(),
            notificationPreset.GetRandomNotificationDescription(), 
            _notificationFireTime);
#elif UNITY_IOS
        StartCoroutine(iosNotificationHandler.RequestAuthorization());

        iosNotificationHandler.SendNotification(
            notificationPreset.GetRandomNotificationTitle(),
            notificationPreset.GetRandomNotificationDescription(), 
            _notificationRepeatTime);
#endif
    }
}