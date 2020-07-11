using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public NotificationPanel NotificationPanelPF;
    public RectTransform NotificationPanelParent;

    public void SpawnNotification(GameResource fromResource)
    {
        SpawnNotification(fromResource.ResourceName, fromResource.Description, fromResource.Graphic);
    }

    public void SpawnNotification(string title, string description, Sprite graphic)
    {
        NotificationPanel newPanel = Instantiate(NotificationPanelPF, NotificationPanelParent);
        newPanel.SetValues(title, description, graphic);
    }
}
