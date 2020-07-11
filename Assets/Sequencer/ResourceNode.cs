using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceNode : MonoBehaviour
{
    public RectTransform MyTransform;
    public Image Graphic;
    public AudioSource MyAudioSource;
    public AudioClip PingSound;

    public GameResource RepresentedResource { get; private set; }

    public void SetResource(GameResource resource, float position)
    {
        RepresentedResource = resource;
        Graphic.sprite = resource.Graphic;

        float yPos = Random.Range(0, 1f);
        MyTransform.anchorMin = new Vector2(position, yPos);
        MyTransform.anchorMax = new Vector2(position, yPos);
        MyTransform.anchoredPosition = Vector2.zero;
    }

    public void PingNode()
    {
        MyAudioSource.pitch = (MyTransform.anchorMin.y * .2f) + .9f;
        MyAudioSource.clip = PingSound;
        MyAudioSource.Play();
    }
}
