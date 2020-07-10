using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCard : MonoBehaviour
{
    string RepresentedResource { get; set; }

    public Image Graphic;
    public Text Name;

    public void SetResource(string resource)
    {
        RepresentedResource = resource;
        Name.text = resource;
    }
}
