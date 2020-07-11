using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image Graphic;
    public Text Name;

    public GameResource RepresentedResource { get; set; }
    Action<ResourceCard> CardDraggedCallback { get; set; }
    Action<ResourceCard> DragEndCallback { get; set; }

    public void SetResource(GameResource resource, Action<ResourceCard> cardDraggedCallback, Action<ResourceCard> dragEndCallback)
    {
        RepresentedResource = resource;
        Name.text = resource.ResourceName;
        Graphic.sprite = resource.Graphic;
        CardDraggedCallback = cardDraggedCallback;
        DragEndCallback = dragEndCallback;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        CardDraggedCallback(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEndCallback(this);
    }
}
