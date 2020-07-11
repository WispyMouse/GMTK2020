using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public NotificationPanel NotificationPanelPF;
    public RectTransform NotificationPanelParent;

    public Sprite ThumbsUpSprite;

    public void SpawnNotification(GameResource fromResource)
    {
        NotificationPanel newPanel = Instantiate(NotificationPanelPF, NotificationPanelParent);
        newPanel.SetValues(fromResource.ResourceName, string.Format(fromResource.Description, fromResource.EffectIntensity), fromResource.Graphic);
        StartCoroutine(HandlePanelShowAndHide(newPanel));
    }

    public void SpawnNotification(string title, string description, Sprite graphic)
    {
        NotificationPanel newPanel = Instantiate(NotificationPanelPF, NotificationPanelParent);
        newPanel.SetValues(title, description, graphic);
        StartCoroutine(HandlePanelShowAndHide(newPanel));
    }

    IEnumerator HandlePanelShowAndHide(NotificationPanel panel)
    {
        yield return panel.Show();
        yield return new WaitForSeconds(2f);
        yield return panel.Hide();
    }
}
