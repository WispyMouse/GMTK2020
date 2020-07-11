using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceNode : MonoBehaviour
{
    public RectTransform MyTransform;
    public Image Graphic;

    public void SetResource(GameResource resource, float position)
    {
        Graphic.sprite = resource.Graphic;

        float yPos = Random.Range(0, 1f);
        MyTransform.anchorMin = new Vector2(position, yPos);
        MyTransform.anchorMax = new Vector2(position, yPos);
        MyTransform.anchoredPosition = Vector2.zero;
    }
}
