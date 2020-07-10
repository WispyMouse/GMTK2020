using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    public Slider SliderInstance;
    public Image SliderImage;
    public Color OKColor;
    public Color LowColor;
    public Color OverflowColor;

    float? OverflowMinimum { get; set; }
    float? LowMaximum { get; set; }

    public void SetLowValue(float? value)
    {
        LowMaximum = value;
    }

    public void SetOverflowValue(float? value)
    {
        OverflowMinimum = value;
    }

    public void SetValue(float value)
    {
        SliderInstance.value = value;

        if (LowMaximum.HasValue && value <= LowMaximum)
        {
            SliderImage.color = LowColor;
        }
        else if (OverflowMinimum.HasValue && value >= OverflowMinimum)
        {
            SliderImage.color = OverflowColor;
        }
        else
        {
            SliderImage.color = OKColor;
        }
    }
}
