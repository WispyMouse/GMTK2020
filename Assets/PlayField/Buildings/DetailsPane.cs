using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailsPane : MonoBehaviour
{
    public Text DetailsText;

    public void Initiate(Reactor fromReactor)
    {
        DetailsText.text = "Basic reactor\nReliable!";
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
