using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class NotificationPreset : ScriptableObject
{
    public List<string> NotificationTitles;
    public List<string> NotificationDescriptions;

    public string GetRandomNotificationTitle()
    {
        int randomIndex = UnityEngine.Random.Range(0, NotificationTitles.Count);
        return NotificationTitles[randomIndex];
    }

    public string GetRandomNotificationDescription()
    {
        int randomIndex = UnityEngine.Random.Range(0, NotificationDescriptions.Count);
        return NotificationDescriptions[randomIndex];
    }
}