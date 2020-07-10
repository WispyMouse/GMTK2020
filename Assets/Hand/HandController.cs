using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public ResourceCard ResourceCardPF;
    public RectTransform HandLocation;

    public void SpawnResourceCard(string resource)
    {
        ResourceCard newCard = Instantiate(ResourceCardPF, HandLocation);
        newCard.SetResource(resource);
    }
}
