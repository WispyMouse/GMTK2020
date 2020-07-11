using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRequesterUI : MonoBehaviour
{
    public ResourceButton ResourceButtonPF;
    public RectTransform ResourceButtonParent;

    public void AddPossibleResource(GameResource resource, System.Action<GameResource> clickedCallback)
    {
        ResourceButton button = Instantiate(ResourceButtonPF, ResourceButtonParent);
        button.SetResource(resource, clickedCallback);
    }
}
