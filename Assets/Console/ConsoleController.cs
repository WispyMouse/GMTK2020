using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour
{
    public Text NotificationText;

    private void Awake()
    {
        NotificationText.text = string.Empty;
    }

    public void PushNotification(string notification)
    {
        string newLine = string.IsNullOrWhiteSpace(NotificationText.text) ? string.Empty : "\n";
        NotificationText.text = NotificationText.text + newLine + notification;
    }
}
