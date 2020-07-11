using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static iTween;

public class HandController : MonoBehaviour
{
    const int MaxHandSize = 10;

    public ResourceCard ResourceCardPF;
    public RectTransform HandLocation;
    public AudioSource MyAudioSource;
    public AudioClip CardPlacedSound;
    public AudioClip CardAcquiredSound;
    public PlaneLineRenderer LineRendererInstance;
    public Camera LineRendererCamera;

    public RectTransform HandPanel;

    List<ResourceCard> CardsInHand { get; set; } = new List<ResourceCard>();
    System.Action HandIsTooFullCallback { get; set; }
    System.Action HandIsNoLongerTooFullCallback { get; set; }
    System.Action<ResourceCard> DragStartCallback { get; set; }

    float SlideTime { get; set; } = .75f;

    public void Initiate(System.Action handIsTooFullCallback, System.Action handIsNoLongerTooFullCallback, System.Action<ResourceCard> dragStartCallback)
    {
        HandIsTooFullCallback = handIsTooFullCallback;
        HandIsNoLongerTooFullCallback = handIsNoLongerTooFullCallback;
        DragStartCallback = dragStartCallback;
    }

    public void AcquireResourceCardFromBoard(GameResource resource, System.Action<ResourceCard> dragEndCallback)
    {
        MyAudioSource.pitch = Random.Range(.95f, 1.05f);
        MyAudioSource.clip = CardAcquiredSound;
        MyAudioSource.Play();
        PlayCard(resource, DragStartCallback, dragEndCallback);
    }

    public void SpawnResourceCardFromSequencer(GameResource resource, System.Action<ResourceCard> dragEndCallback)
    {
        PlayCard(resource, DragStartCallback, dragEndCallback);
    }

    void PlayCard(GameResource resource, System.Action<ResourceCard> dragStartCallback, System.Action<ResourceCard> dragEndCallback)
    {
        ResourceCard newCard = Instantiate(ResourceCardPF, HandLocation);
        newCard.transform.position = HandLocation.position + Vector3.right * 12f;
        newCard.SetResource(resource, dragStartCallback, CardDragged, (ResourceCard card) => { CardDropped(card); dragEndCallback(card); });
        CardsInHand.Add(newCard);

        if (CardsInHand.Count() == MaxHandSize + 1) // we just drew the first overflowing card
        {
            HandIsTooFullCallback();
        }

        AdjustCardsInHandPosition();
    }

    public void ConsumeCard(ResourceCard toConsume)
    {
        MyAudioSource.pitch = Random.Range(.95f, 1.05f);
        MyAudioSource.clip = CardPlacedSound;
        MyAudioSource.Play();
        CardsInHand.Remove(toConsume);
        Destroy(toConsume.gameObject);

        if (CardsInHand.Count() == MaxHandSize) // we just went down to less than overflowing
        {
            HandIsNoLongerTooFullCallback();
        }

        AdjustCardsInHandPosition();
    }

    void CardDragged(ResourceCard dragging)
    {
        LineRendererInstance.enabled = true;
        Vector3 target = LineRendererCamera.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        LineRendererInstance.ClearSegments();
        LineRendererInstance.AddSegment(dragging.GetCenter(), target);
    }

    void CardDropped(ResourceCard dropped)
    {
        LineRendererInstance.enabled = false;
    }

    void AdjustCardsInHandPosition()
    {
        for (int ii = 0; ii < CardsInHand.Count; ii++)
        {
            CardsInHand[ii].TweenToHandSpot(ii, HandLocation);
        }
    }

    public void Hide()
    {
    }

    public void Show()
    {
        // Couldn't quite get this to work due to screenspace UIs being wonky
        // so just, appear
    }
}
