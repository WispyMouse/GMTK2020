using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequencerUI : MonoBehaviour
{
    public RectTransform ResourceNodeParent;
    public RectTransform SequencerBar;
    public AudioSource MyAudioSource;

    public ResourceNode ResourceNodePF;

    public AudioClip Ping;

    public void NudgeBar(float progress)
    {
        SequencerBar.anchorMin = new Vector2(progress, 0);
        SequencerBar.anchorMax = new Vector2(progress, 1f);
        SequencerBar.anchoredPosition = Vector2.zero;
    }

    public ResourceNode AddResource(GameResource resource, float position)
    {
        ResourceNode newNode = Instantiate(ResourceNodePF, ResourceNodeParent);
        newNode.SetResource(resource, position);
        return newNode;
    }
}
