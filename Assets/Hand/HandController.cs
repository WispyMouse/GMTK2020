using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public ResourceCard ResourceCardPF;
    public RectTransform HandLocation;

    public void SpawnResourceCard(string resource, System.Action<ResourceCard> dragEndCallback)
    {
        ResourceCard newCard = Instantiate(ResourceCardPF, HandLocation);
        newCard.SetResource(resource, dragEndCallback);
    }

    public void ConsumeCard(ResourceCard toConsume)
    {
        Destroy(toConsume.gameObject);
    }
}
