using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    public Image FillImage;
    public Image BackgroundImage;
    public Image SliderImage;

    public Color OKColor;
    public Color LowColor;
    public Color OverflowColor;
    public Color OkayFillColor;
    public Color WouldOverflowFillColor;

    float LowWarning { get; set; } = .2f;

    public void SetValue(float value, float maxValue, float maximumPossibleValue, float fill)
    {
        BackgroundImage.fillAmount = maxValue / maximumPossibleValue;
        SliderImage.fillAmount = (value / maxValue) * (maxValue / maximumPossibleValue);
        FillImage.fillAmount = SliderImage.fillAmount + (fill / maxValue) * (maxValue / maximumPossibleValue);

        if (value / maxValue <= LowWarning)
        {
            SliderImage.color = LowColor;
        }
        else if (value > maxValue)
        {
            SliderImage.color = OverflowColor;
        }
        else
        {
            SliderImage.color = OKColor;
        }

        if (value + fill > maxValue)
        {
            FillImage.color = WouldOverflowFillColor;
        }
        else
        {
            FillImage.color = OkayFillColor;

        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
