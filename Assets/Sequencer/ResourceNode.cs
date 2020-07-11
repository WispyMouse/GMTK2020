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

    float PingTime { get; set; } = .35f;
    float PlayedSizeIncrease { get; set; } = .8f;
    float SequencerPingSizeIncrease { get; set; } = .5f;

    public void SetResource(GameResource resource, float position)
    {
        RepresentedResource = resource;
        Graphic.sprite = resource.Graphic;

        float yPos = Random.Range(0, 1f);
        MyTransform.anchorMin = new Vector2(position, yPos);
        MyTransform.anchorMax = new Vector2(position, yPos);
        MyTransform.anchoredPosition = Vector2.zero;

        StartCoroutine(PingAnimation(PlayedSizeIncrease));
    }

    public void PingNode()
    {
        MyAudioSource.pitch = (MyTransform.anchorMin.y * .2f) + .9f;
        MyAudioSource.clip = PingSound;
        MyAudioSource.Play();

        StartCoroutine(PingAnimation(SequencerPingSizeIncrease));
    }

    IEnumerator PingAnimation(float intensity)
    {
        float time = 0;

        while (time <= PingTime)
        {
            time += Time.deltaTime;
            Graphic.transform.localScale = Vector3.one * (Mathf.Sin(time / PingTime * 180f * Mathf.Deg2Rad) * intensity + 1f);
            yield return new WaitForEndOfFrame();
        }

        Graphic.transform.localScale = Vector3.one;
    }
}
