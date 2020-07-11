using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    const int MaxHandSize = 10;

    public ResourceCard ResourceCardPF;
    public RectTransform HandLocation;
    public AudioSource MyAudioSource;
    public AudioClip CardPlacedSound;
    public AudioClip CardAcquiredSound;

    int CardsInHand { get; set; } = 0;
    System.Action HandIsTooFullCallback { get; set; }
    System.Action HandIsNoLongerTooFullCallback { get; set; }

    public void Initiate(System.Action handIsTooFullCallback, System.Action handIsNoLongerTooFullCallback)
    {
        HandIsTooFullCallback = handIsTooFullCallback;
        HandIsNoLongerTooFullCallback = handIsNoLongerTooFullCallback;
    }

    public void AcquireResourceCardFromBoard(GameResource resource, System.Action<ResourceCard> dragEndCallback)
    {
        MyAudioSource.pitch = Random.Range(.95f, 1.05f);
        MyAudioSource.clip = CardAcquiredSound;
        MyAudioSource.Play();
        PlayCard(resource, dragEndCallback);
    }

    public void SpawnResourceCardFromSequencer(GameResource resource, System.Action<ResourceCard> dragEndCallback)
    {
        PlayCard(resource, dragEndCallback);
    }

    void PlayCard(GameResource resource, System.Action<ResourceCard> dragEndCallback)
    {
        ResourceCard newCard = Instantiate(ResourceCardPF, HandLocation);
        newCard.SetResource(resource, dragEndCallback);
        CardsInHand++;

        if (CardsInHand == MaxHandSize + 1) // we just drew the first overflowing card
        {
            HandIsTooFullCallback();
        }
    }

    public void ConsumeCard(ResourceCard toConsume)
    {
        MyAudioSource.pitch = Random.Range(.95f, 1.05f);
        MyAudioSource.clip = CardPlacedSound;
        MyAudioSource.Play();
        Destroy(toConsume.gameObject);
        CardsInHand--;

        if (CardsInHand == MaxHandSize) // we just went down to less than overflowing
        {
            HandIsNoLongerTooFullCallback();
        }
    }
}
