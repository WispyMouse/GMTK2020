using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalamityClock : MonoBehaviour
{
    public Image ProgressingImage;

    bool Broken { get; set; } = false;
    float MaxTime { get; set; } = 3f;
    float CurTime { get; set; } = 3f;
    System.Action MaxOutTimer { get; set; } 

    public void Initiate(System.Action maxOutTimer)
    {
        gameObject.SetActive(true);
        CurTime = 0;
        this.MaxOutTimer = maxOutTimer;
    }

    public void Resolved()
    {
        if (Broken)
        {
            return;
        }

        gameObject.SetActive(false);
    }

    void Break()
    {
        Broken = true;
    }

    private void Update()
    {
        if (Broken)
        {
            return;
        }

        CurTime += Time.deltaTime;
        ProgressingImage.fillAmount = CurTime / MaxTime;

        if (CurTime >= MaxTime)
        {
            Break();
            MaxOutTimer();
        }
    }
}
