using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceButton : MonoBehaviour
{
    public Text Label;
    public Button ButtonBehavior;

    GameResource RepresentedResource { get; set; }
    System.Action<GameResource> ClickedCallback { get; set; }

    public void SetResource(GameResource resource, System.Action<GameResource> clickedCallback)
    {
        RepresentedResource = resource;
        Label.text = resource.ResourceName;
        ButtonBehavior.onClick.AddListener(() => { Clicked(); });
        ClickedCallback = clickedCallback;
    }

    void Clicked()
    {
        Debug.Log($"Requesting Resource Clicked: {RepresentedResource}");
        ClickedCallback(RepresentedResource);
    }
}
