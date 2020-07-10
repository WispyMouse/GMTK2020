using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequencerController : MonoBehaviour
{
    public SequencerUI SequencerUIInstance;

    float sequencerSpeed { get; set; } = .2f;
    float sequencerProgress { get; set; } = 0f;

    float? nextResourceTime { get; set; }
    SortedDictionary<float, string> ResourcesOnSequencer { get; set; } = new SortedDictionary<float, string>();
    System.Action<string> SequencerResourcePopCallback { get; set; }

    public void Initiate(System.Action<string> sequencerResorucePopCallback)
    {
        SequencerResourcePopCallback = sequencerResorucePopCallback;
    }

    private void Update()
    {
        float newProgress = (sequencerProgress + Time.deltaTime * sequencerSpeed);
        bool looped = newProgress >= 1f;
        newProgress %= 1f;

        SequencerUIInstance.NudgeBar(newProgress);

        // for each resource that is further on the timeline than we used to be, and behind where we are now, pop
        // if we looped, and we used to be behind a resource, then pop
        if (nextResourceTime.HasValue && sequencerProgress < nextResourceTime.Value
            && (newProgress >= nextResourceTime.Value
            || (sequencerProgress < nextResourceTime.Value && looped)))
        {
            foreach (string resource in ResourcesOnSequencer.Where(r => sequencerProgress < r.Key && (looped || newProgress >= r.Key)).Select(kvp => kvp.Value))
            {
                SequencerResourcePopCallback(resource);
            }

            // find the next resource after our new position
            // if there isn't one, then find the first on the timeline
            nextResourceTime = ResourcesOnSequencer.FirstOrDefault(r => r.Key > newProgress).Key;

            if (!nextResourceTime.HasValue && ResourcesOnSequencer.Any())
            {
                nextResourceTime = ResourcesOnSequencer.First().Key;
            }
        }

        sequencerProgress = newProgress;
    }

    public void AddResource(string resource, float? time = null)
    {
        SequencerUIInstance.AddResource(resource, time ?? sequencerProgress);
        ResourcesOnSequencer.Add(time ?? sequencerProgress, resource);

        if (!nextResourceTime.HasValue)
        {
            nextResourceTime = time ?? sequencerProgress;
        }
    }
}
