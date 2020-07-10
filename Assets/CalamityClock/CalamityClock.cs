﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalamityClock : MonoBehaviour
{
    public GameObject VisualToggle;
    public Image ProgressingImage;
    public Text ReasonText;

    bool Broken { get; set; } = false;
    float MaxTime { get; set; } = 6f;
    float CurTime { get; set; } = 0f;
    float CoolingSpeed { get; set; } = 1f;
    System.Action MaxOutTimer { get; set; }
    List<string> Problems { get; set; } = new List<string>();

    public void Initiate(System.Action maxOutTimer)
    {
        CurTime = 0;
        this.MaxOutTimer = maxOutTimer;
    }

    public void AddReason(string toAdd)
    {
        if (Broken)
        {
            return;
        }

        VisualToggle.gameObject.SetActive(true);
        Problems.Add(toAdd);
        if (Problems.Count == 1) // this is the first problem
        {
            CurTime = 0;
        }

        UpdateLabel();
    }

    public void RemoveReason(string toRemove)
    {
        if (Broken)
        {
            return;
        }

        Problems.Remove(toRemove);
        if (Problems.Count == 0) // no more problems
        {
            VisualToggle.gameObject.SetActive(false);
        }

        UpdateLabel();
    }

    void Break()
    {
        Broken = true;
    }

    void UpdateLabel()
    {
        ReasonText.text = string.Join("\n", Problems);
    }

    private void Update()
    {
        if (Broken)
        {
            return;
        }

        if (Problems.Count == 0)
        {
            // No problems, so let's reduce the panic timer
            CurTime = Mathf.Max(0, CurTime - Time.deltaTime * CoolingSpeed);
        }
        else
        {
            // We have problems, so let's increase the panic timer
            CurTime += Time.deltaTime;
            ProgressingImage.fillAmount = CurTime / MaxTime;

            if (CurTime >= MaxTime)
            {
                Break();
                MaxOutTimer();
            }
        }
    }
}
