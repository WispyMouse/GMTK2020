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

    public AudioSource PanicMusicSource;
    public AudioSource UsualMusicSource;
    public AudioSource PostGameMusicSource;

    public Color PanicColor;
    public Color CoolingColor;
    public Image ClockBackground;
    Color ColorOne;
    Color ColorTwo;
    float ColorProgress;

    float MaxMusicProgress { get; } = 3f;
    float MusicProgress { get; set; } = 0f;
    float UsualMusicGainSpeed { get; } = 1f;
    float PanicMusicGainSpeed { get; } = 3f;

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
        PanicMusicSource.volume = 0;
        UsualMusicSource.volume = 1f;
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

        UpdateLabel();
    }

    void Break()
    {
        Broken = true;
        PanicMusicSource.Stop();
        UsualMusicSource.Stop();
        PostGameMusicSource.Play();
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
            MusicProgress = Mathf.Max(0, MusicProgress - Time.deltaTime * UsualMusicGainSpeed);

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
            MusicProgress = Mathf.Min(MaxMusicProgress, MusicProgress + Time.deltaTime * PanicMusicGainSpeed);

            if (CurTime >= MaxTime)
            {
                Break();
            }
        }

        UsualMusicSource.volume = 1f - (MusicProgress / MaxMusicProgress);
        PanicMusicSource.volume = MusicProgress / MaxMusicProgress;
    }
}
