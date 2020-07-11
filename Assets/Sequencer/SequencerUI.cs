using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static iTween;

public class SequencerUI : MonoBehaviour
{
    public RectTransform SequencerPanel;
    public RectTransform ResourceNodeParent;
    public RectTransform SequencerBar;

    public ResourceNode ResourceNodePF;

    public AudioClip Ping;

    float SlideTime { get; set; } = .75f;

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

    public void Hide()
    {
        SequencerPanel.transform.position = Vector3.zero;
    }

    public void Show()
    {
        Hashtable showTable = new Hashtable();
        showTable.Add("position", Vector3.up * 200f);
        showTable.Add("time", SlideTime);
        showTable.Add("easetype", EaseType.easeOutBack);
        iTween.MoveTo(SequencerPanel.gameObject, showTable);
    }
}
