using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static iTween;

public class CalamityClock : MonoBehaviour
{
    public GameObject VisualToggle;
    public Image ProgressingImage;
    public Text ReasonText;

    public AudioSource BackgroundMusicSource;
    public AudioClip PeacefulMusic;
    public AudioClip NormalMusic;
    public AudioClip ExcitedMusic;

    public Color PanicColor;
    public Color CoolingColor;
    public Image ClockBackground;
    Color ColorOne;
    Color ColorTwo;
    float ColorProgress;

    bool Broken { get; set; } = false;
    float MaxTime { get; set; } = 6f;
    float CurTime { get; set; } = 0f;
    float CoolingSpeed { get; set; } = 1f;
    System.Action MaxOutTimerCallback { get; set; }
    List<string> Problems { get; set; } = new List<string>();

    public void Initiate(System.Action maxOutTimer)
    {
        ColorOne = CoolingColor;
        ColorTwo = ColorOne;
        CurTime = 0;
        this.MaxOutTimerCallback = maxOutTimer;
        BackgroundMusicSource.clip = NormalMusic;
        BackgroundMusicSource.Stop();
        BackgroundMusicSource.Play();
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
            BackgroundMusicSource.clip = ExcitedMusic;
            BackgroundMusicSource.Stop();
            BackgroundMusicSource.Play();

            Hashtable showTable = new Hashtable();
            showTable.Add("amount", Vector3.left * 200f);
            showTable.Add("time", 1f);
            showTable.Add("easetype", EaseType.easeInBack);
            iTween.MoveAdd(this.gameObject, showTable);
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
            BackgroundMusicSource.clip = NormalMusic;
            BackgroundMusicSource.Stop();
            BackgroundMusicSource.Play();
        }

        UpdateLabel();
    }

    void Break()
    {
        Broken = true;
        BackgroundMusicSource.clip = PeacefulMusic;
        BackgroundMusicSource.Play();
        MaxOutTimerCallback();
    }

    void UpdateLabel()
    {
        if (Problems.Any())
        {
            ReasonText.text = string.Join("\n", Problems);
        }
        else
        {
            ReasonText.text = "Calming down";
        }
    }

    private void Update()
    {
        if (Broken)
        {
            return;
        }

        ColorProgress += Time.deltaTime;
        ProgressingImage.fillAmount = CurTime / MaxTime;

        if (Problems.Count == 0)
        {
            if (CurTime > 0)
            {
                // No problems, so let's reduce the panic timer
                CurTime = Mathf.Max(0, CurTime - Time.deltaTime * CoolingSpeed);
                ClockBackground.color = Color.Lerp(PanicColor, CoolingColor, ColorProgress);

                if (CurTime == 0)
                {
                    // We're all cooled off
                    ColorOne = CoolingColor;
                    ColorTwo = CoolingColor;
                    Hashtable showTable = new Hashtable();
                    showTable.Add("amount", Vector3.right * 200f);
                    showTable.Add("time", 1f);
                    showTable.Add("easetype", EaseType.easeInBack);
                    iTween.MoveAdd(this.gameObject, showTable);
                }
            }
        }
        else
        {
            // We have problems, so let's increase the panic timer
            ClockBackground.color = Color.Lerp(CoolingColor, PanicColor, Mathf.PingPong(ColorProgress * 2f, 1f));
            CurTime += Time.deltaTime;

            if (CurTime >= MaxTime)
            {
                Break();
            }
        }
    }
}
