using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static iTween;

public class ResourceCard : MonoBehaviour
{
    public RectTransform MoveableBody;
    public Image Graphic;

    public Image CaffeineSlice;
    public Image SugarSlice;
    public Image CarbSlice;

    public GameResource RepresentedResource { get; set; }
    float SlideTime { get; set; } = .55f;

    public void SetResource(GameResource resource)
    {
        RepresentedResource = resource;
        Graphic.sprite = resource.Graphic;

        CaffeineSlice.gameObject.SetActive(resource.IsCaffeine);
        SugarSlice.gameObject.SetActive(resource.IsSugar);
        CarbSlice.gameObject.SetActive(resource.IsCarb);
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
