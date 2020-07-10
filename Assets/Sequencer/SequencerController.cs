using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerController : MonoBehaviour
{
    public SequencerUI SequencerUIInstance;

    float sequencerSpeed { get; set; } = .3f;
    float sequencerProgress { get; set; } = 0f;

    private void Update()
    {
        sequencerProgress = (sequencerProgress + Time.deltaTime * sequencerSpeed) % 1f;
        SequencerUIInstance.NudgeBar(sequencerProgress);
    }

    public void AddResource(string resource, float? time = null)
    {
        SequencerUIInstance.AddResource(resource, time ?? sequencerProgress);
    }
}
