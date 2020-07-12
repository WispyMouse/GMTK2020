using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    public HandController HandControllerInstance;
    public Button ClearButton;
    public Text ClearButtonText;

    int ClearMaxCooldown { get; } = 2;
    int ClearCurCooldown { get; set; } = 0;

    public void EmptyHand()
    {
        HandControllerInstance.EmptyHand();
        ClearCurCooldown = ClearMaxCooldown;
        ClearButton.interactable = false;
        ClearButtonText.text = $"Empty Hand\nCD: {ClearCurCooldown}";
    }

    public void CyclePasses()
    {
        ClearCurCooldown = Mathf.Max(0, ClearCurCooldown - 1);

        if (ClearCurCooldown == 0)
        {
            ClearButton.interactable = true;
            ClearButtonText.text = "Empty Hand";
        }
        else
        {
            ClearButtonText.text = $"Empty Hand\nCD: {ClearCurCooldown}";
        }
    }
}
