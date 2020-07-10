using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRequesterUI : MonoBehaviour
{
    public ResourceButton ResourceButtonPF;
    public RectTransform ResourceButtonParent;

    public void AddPossibleResource(string resource, System.Action<string> clickedCallback)
    {
        ResourceButton button = Instantiate(ResourceButtonPF, ResourceButtonParent);
        button.SetResource(resource, clickedCallback);
    }
}
