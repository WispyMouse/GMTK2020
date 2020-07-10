using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public RectTransform MyTransform;

    public void SetResource(string resource, float position)
    {
        // set graphic, callbacks

        float yPos = Random.Range(0, 1f);
        MyTransform.anchorMin = new Vector2(position, yPos);
        MyTransform.anchorMax = new Vector2(position, yPos);
        MyTransform.anchoredPosition = Vector2.zero;
    }
}
