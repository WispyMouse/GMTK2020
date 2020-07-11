using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour
{
    public Text BigText;
    public Text ExplanationText;
    public Image Graphic;

    public void SetValues(string title, string description, Sprite graphic)
    {
        BigText.text = title;
        ExplanationText.text = description;
        Graphic.sprite = graphic;
    }
}
