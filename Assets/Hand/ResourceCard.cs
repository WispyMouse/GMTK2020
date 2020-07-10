using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image Graphic;
    public Text Name;

    string RepresentedResource { get; set; }
    Vector2 dragOffset { get; set; }
    System.Action<ResourceCard> DragEndCallback { get; set; }

    public void SetResource(string resource, System.Action<ResourceCard> dragEndCallback)
    {
        RepresentedResource = resource;
        Name.text = resource;
        DragEndCallback = dragEndCallback;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEndCallback(this);
    }
}
