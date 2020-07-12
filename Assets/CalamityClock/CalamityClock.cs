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

    public NotificationController NotificationControllerInstance;

    public Color PanicColor;
    public Color CoolingColor;
    public Image ClockBackground;
    float ColorProgress;

    float MaxMusicProgress { get; } = 1f;
    float MusicProgress { get; set; } = 0f;
    float UsualMusicGainSpeed { get; } = .33f;
    float PanicMusicGainSpeed { get; } = 10f;

    bool Broken { get; set; } = false;
    float MaxTime { get; set; } = 6f;
    float CurTime { get; set; } = 0f;
    float CoolingSpeed { get; set; } = 1f;
    System.Action MaxOutTimerCallback { get; set; }
    List<string> Problems { get; set; } = new List<string>();

    public void Initiate(System.Action maxOutTimer)
    {
        CurTime = 0;
        this.MaxOutTimerCallback = maxOutTimer;
        PanicMusicSource.volume = 0;
        UsualMusicSource.volume = 1f;
        ClockBackground.color = CoolingColor;
    }

    public void AddReason(string toAdd)
    {
        if (Broken)
        {
            return;
        }

        NotificationControllerInstance.SpawnNotification($"<color=red> > > {toAdd}!!</color>");

        VisualToggle.gameObject.SetActive(true);
        Problems.Add(toAdd);
        if (Problems.Count == 1) // this is the first problem
        {
            // something new?
            // used to be where the clock appeared in at
        }
    }

    public void RemoveReason(string toRemove)
    {
        if (Broken)
        {
            return;
        }

        Problems.Remove(toRemove);
    }

    void Break()
    {
        Broken = true;
        PanicMusicSource.Stop();
        UsualMusicSource.Stop();
        PostGameMusicSource.Play();
        MaxOutTimerCallback();
    }

    private void Update()
    {
        if (Broken)
        {
            return;
        }

        ColorProgress += Time.deltaTime * .25f;
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
                    ClockBackground.color = CoolingColor;
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

    public List<string> GetProblems()
    {
        return Problems;
    }
}
