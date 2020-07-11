using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static iTween;

public class ResourceCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform MoveableBody;
    public Image Graphic;
    public Text Name;

    public GameResource RepresentedResource { get; set; }
    Action<ResourceCard> DragStartCallback { get; set; }
    Action<ResourceCard> CardDraggedCallback { get; set; }
    Action<ResourceCard> DragEndCallback { get; set; }
    float SlideTime { get; set; } = .55f;

    public void SetResource(GameResource resource, Action<ResourceCard> cardDragStartCallback, Action<ResourceCard> cardDraggedCallback, Action<ResourceCard> dragEndCallback)
    {
        RepresentedResource = resource;
        Name.text = resource.ResourceName;
        Graphic.sprite = resource.Graphic;
        DragStartCallback = cardDragStartCallback;
        CardDraggedCallback = cardDraggedCallback;
        DragEndCallback = dragEndCallback;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragStartCallback(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        CardDraggedCallback(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEndCallback(this);
    }

    public void TweenToHandSpot(int index, RectTransform parentTransform)
    {
        float relativeSpace = index;
        Vector3 targetPosition = parentTransform.position + Vector3.right * 1.2f * relativeSpace; // I don't know why, but it's off by 20%; no time to figure this out during the jam, just magic number it

        Hashtable showTable = new Hashtable();
        showTable.Add("position", targetPosition);
        showTable.Add("time", SlideTime);
        showTable.Add("easetype", EaseType.easeOutBounce);
        iTween.MoveTo(MoveableBody.gameObject, showTable);
    }

    public Vector3 GetCenter()
    {
        return Graphic.transform.position;
    }
}
