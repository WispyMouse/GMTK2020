using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    const int MaxHandSize = 10;

    public ResourceCard ResourceCardPF;
    public RectTransform HandLocation;

    int CardsInHand { get; set; } = 0;
    System.Action HandIsTooFullCallback { get; set; }
    System.Action HandIsNoLongerTooFullCallback { get; set; }

    public void Initiate(System.Action handIsTooFullCallback, System.Action handIsNoLongerTooFullCallback)
    {
        HandIsTooFullCallback = handIsTooFullCallback;
        HandIsNoLongerTooFullCallback = handIsNoLongerTooFullCallback;
    }

    public void SpawnResourceCard(string resource, System.Action<ResourceCard> dragEndCallback)
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
        Destroy(toConsume.gameObject);

        if (CardsInHand == MaxHandSize) // we just went down to less than overflowing
        {
            HandIsNoLongerTooFullCallback();
        }
    }
}
