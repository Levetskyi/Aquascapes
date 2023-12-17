using System.Collections;
using UnityEngine;
using System;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class IOSNotificationHandler : MonoBehaviour
{
#if UNITY_IOS
    public IEnumerator RequestAuthorization()
    {
        using var request = new AuthorizationRequest(AuthorizationOption.Alert | AuthorizationOption.Badge, true);
        
        while (!request.IsFinished) 
        {
            yield return null;
        }
    }

    public void SendNotification(string title, string body, int fireTimeInHous)
    {
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new TimeSpan(fireTimeInHous, 0, 0),
            Repeats = false,

        };

        var notification = new iOSNotification()
        {
            Title = title,
            Body = body,
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "default_category",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
}