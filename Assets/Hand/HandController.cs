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

    public RectTransform HandPanel;

    List<ResourceCard> CardsInHand { get; set; } = new List<ResourceCard>();
    System.Action HandIsTooFullCallback { get; set; }
    System.Action HandIsNoLongerTooFullCallback { get; set; }

    public void Initiate(System.Action handIsTooFullCallback, System.Action handIsNoLongerTooFullCallback)
    {
        HandIsTooFullCallback = handIsTooFullCallback;
        HandIsNoLongerTooFullCallback = handIsNoLongerTooFullCallback;
    }

    public void AcquireResourceCardFromBoard(GameResource resource)
    {
        /*
        MyAudioSource.pitch = Random.Range(.95f, 1.05f);
        MyAudioSource.clip = CardAcquiredSound;
        MyAudioSource.Play();
        */
        PlayCard(resource);
    }

    public void SpawnResourceCardFromSequencer(GameResource resource)
    {
        PlayCard(resource);
    }

    void PlayCard(GameResource resource)
    {
        ResourceCard newCard = Instantiate(ResourceCardPF, HandLocation);
        newCard.transform.position = HandLocation.position + Vector3.right * 12f;
        newCard.SetResource(resource);
        CardsInHand.Add(newCard);

        if (CardsInHand.Count() == MaxHandSize + 1) // we just drew the first overflowing card
        {
            HandIsTooFullCallback();
        }
        else if (CardsInHand.Count() == 1) // this is our first card
        {
            MyAudioSource.clip = resource.ActiveCardSound;
            MyAudioSource.Play();
        }

        AdjustCardsInHandPosition();
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

    public ResourceCard GetActiveCard()
    {
        return CardsInHand.FirstOrDefault();
    }

    public void ConsumeActiveCard()
    {
        /*
        MyAudioSource.pitch = Random.Range(.95f, 1.05f);
        MyAudioSource.clip = CardPlacedSound;
        MyAudioSource.Play();
        */
        ResourceCard removedCard = GetActiveCard();
        CardsInHand.RemoveAt(0);
        Destroy(removedCard.gameObject);

        if (CardsInHand.Count() == MaxHandSize) // we just went down to less than overflowing
        {
            HandIsNoLongerTooFullCallback();
        }
        else if (CardsInHand.Count > 0)
        {
            MyAudioSource.clip = CardsInHand[0].RepresentedResource.ActiveCardSound;
            MyAudioSource.Play();
        }

        AdjustCardsInHandPosition();
    }

    public void EmptyHand()
    {
        bool handWasTooFull = CardsInHand.Count > MaxHandSize;

        for (int ii = CardsInHand.Count - 1; ii >= 0; ii--)
        {
            Destroy(CardsInHand[ii].gameObject);
        }

        CardsInHand.Clear();

        if (handWasTooFull)
        {
            HandIsNoLongerTooFullCallback();
        }

        AdjustCardsInHandPosition();
    }
}
