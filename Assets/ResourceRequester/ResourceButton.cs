using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceButton : MonoBehaviour
{
    public Text Label;
    public Button ButtonBehavior;

    string RepresentedResource { get; set; }
    System.Action<string> ClickedCallback { get; set; }

    public void SetResource(string resource, System.Action<string> clickedCallback)
    {
        RepresentedResource = resource;
        Label.text = resource;
        ButtonBehavior.onClick.AddListener(() => { Clicked(); });
        ClickedCallback = clickedCallback;
    }

    void Clicked()
    {
        Debug.Log($"Requesting Resource Clicked: {RepresentedResource}");
        ClickedCallback(RepresentedResource);
    }
}
