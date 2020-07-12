using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public ConsoleController ConsoleControllerInstance;

    public NotificationPanel NotificationPanelPF;
    public RectTransform NotificationPanelParent;
    int activePanels { get; set; } = 0;

    public Sprite ThumbsUpSprite;

    public void SpawnNotification(GameResource fromResource)
    {
        ConsoleControllerInstance.PushNotification($"New Resource! {fromResource.ResourceName}");
        // SpawnNotification(fromResource.ResourceName, string.Format(fromResource.Description, fromResource.EffectIntensity), fromResource.Graphic);
    }

    public void SpawnNotification(Reactor reactor)
    {
        ConsoleControllerInstance.PushNotification($"Reactor Online! {reactor.name}");
        // SpawnNotification($"Reactor Online!\n{reactor.name}", string.Format("Max Fuel {0}\nDrain Rate {1}", reactor.MaxFuel, reactor.DrainRate), ThumbsUpSprite);
    }

    public void SpawnNotification(string title, string description, Sprite graphic)
    {
        NotificationPanel newPanel = Instantiate(NotificationPanelPF, NotificationPanelParent);
        newPanel.SetValues(title, description, graphic);
        StartCoroutine(HandlePanelShowAndHide(newPanel));
    }

    public void SpawnNotification(string notification)
    {
        ConsoleControllerInstance.PushNotification(notification);
    }

    IEnumerator HandlePanelShowAndHide(NotificationPanel panel)
    {
        activePanels++;
        yield return panel.Show(activePanels);
        yield return new WaitForSeconds(2f);
        yield return panel.Hide();
        Destroy(panel.gameObject);
        activePanels--;
    }
}
