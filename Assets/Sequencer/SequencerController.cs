﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SequencerController : MonoBehaviour
{
    public SequencerUI SequencerUIInstance;

    float sequencerSpeed { get; set; } = .15f;
    float sequencerProgress { get; set; } = 0f;

    float? nextResourceTime { get; set; }

    /// <summary>
    /// What resources are on the Sequencer, at what time.
    /// The Key is the time, the Value is the resource.
    /// </summary>
    SortedDictionary<float, ResourceNode> ResourcesOnSequencer { get; set; } = new SortedDictionary<float, ResourceNode>();

    System.Action<GameResource> SequencerResourcePopCallback { get; set; }
    System.Action CyclePassesCallback { get; set; }

    public void Initiate(System.Action<GameResource> sequencerResourcePopCallback, System.Action cyclePassesCallback)
    {
        SequencerResourcePopCallback = sequencerResourcePopCallback;
        CyclePassesCallback = cyclePassesCallback;
    }

    private void Update()
    {
        if (GameplayController.GameIsStopped)
        {
            return;
        }

        float newProgress = (sequencerProgress + Time.deltaTime * sequencerSpeed);
        newProgress %= 1f;

        if (Looped(sequencerProgress, newProgress))
        {
            SequencerUIInstance.NudgeBar(newProgress); // may have some animation difference?
            CyclePassesCallback();
        }
        else
        {
            SequencerUIInstance.NudgeBar(newProgress);
        }
        

        // for each resource that is further on the timeline than we used to be, and behind where we are now, pop
        // if we looped, and we used to be behind a resource, then pop
        if (nextResourceTime.HasValue && ShouldProcSequence(nextResourceTime.Value, sequencerProgress, newProgress))
        {
            Debug.Log($"Spawning resources, progress is {sequencerProgress} and newProgress is {newProgress}");

            foreach (ResourceNode resource in ResourcesOnSequencer.Where(kvp => ShouldProcSequence(kvp.Key, sequencerProgress, newProgress)).Select(kvp => kvp.Value))
            {
                Debug.Log($"Resource Spawning from Sequencer: {resource.RepresentedResource.ResourceName}");
                SequencerResourcePopCallback(resource.RepresentedResource);
                resource.PingNode();
            }

            // find the next resource after our new position
            // if there isn't one, then find the first on the timeline
            if (ResourcesOnSequencer.Any(r => r.Key > newProgress))
            {
                nextResourceTime = ResourcesOnSequencer.First(r => r.Key > newProgress).Key;
            }
            else
            {
                nextResourceTime = ResourcesOnSequencer.First().Key;
            }

            if (nextResourceTime.HasValue)
            {
                Debug.Log($"Next Resource Time: {nextResourceTime.Value}");
            }
        }

        sequencerProgress = newProgress;
    }

    public void AddResource(GameResource resource, float? time = null)
    {
        if (!time.HasValue)
        {
            time = Random.Range(.05f, .95f);
        }

        ResourceNode newNode = SequencerUIInstance.AddResource(resource, time.Value);
        ResourcesOnSequencer.Add(time.Value, newNode);

        // Detect if we should update the next resource time to this one
        // There were good intentions with this code, but it's gotten away from us a bit
        if (!nextResourceTime.HasValue // We don't have a value yet, use this one
            || (sequencerProgress < time.Value && time < nextResourceTime.Value) // We have a value, and it's after this new one
            || (Looped(sequencerProgress, time.Value) && Looped(sequencerProgress, nextResourceTime.Value) && time < nextResourceTime.Value)) // When we loop, this value is next
        {
            nextResourceTime = time;
        }

        Debug.Log($"Resource Added to Sequencer: {resource.ResourceName}");
    }


    /// <summary>
    /// While this function seems really obvious, it does make the code a pinch easier to read to canonize how to handle it
    /// </summary>
    bool Looped(float prevValue, float curValue)
    {
        return curValue < prevValue;
    }

    bool ShouldProcSequence(float triggerValue, float prevValue, float curValue)
    {
        if (triggerValue > prevValue && triggerValue <= curValue)
        {
            return true;
        }

        if (Looped(prevValue, curValue) && (triggerValue > prevValue || triggerValue <= curValue))
        {
            return true;
        }

        return false;
    }

    public void Hide()
    {
        SequencerUIInstance.Hide();
    }

    public void Show()
    {
        SequencerUIInstance.Show();
    }
}
