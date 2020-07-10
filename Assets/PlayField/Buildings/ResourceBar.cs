using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBar : MonoBehaviour
{
    public Slider SliderInstance;

    public void SetValue(float value)
    {
        SliderInstance.value = value;
    }
}
