using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public ResourceRequesterUI ResourceRequesterUIInstance;
    public SequencerController SequencerControllerInstance;
    public PlayFieldController PlayFieldControllerInstance;
    public HandController HandControllerInstance;

    public CalamityClock CalamityClockInstance;
    public GameOverMenu GameOverMenuInstance;

    private void Start()
    {
        ResourceRequesterUIInstance.AddPossibleResource("Juice", ResourceRequested);
    }

    void ResourceRequested(string resource)
    {
        HandControllerInstance.SpawnResourceCard(resource, ResourceCardDropped);
        SequencerControllerInstance.AddResource(resource);
    }

    void ResourceCardDropped(ResourceCard card)
    {
        Reactor hoveredReactor = PlayFieldControllerInstance.GetHoveredReactor();
        
        if (hoveredReactor != null)
        {
            // apply the effect of the card or something
            hoveredReactor.Fuel(card);
            HandControllerInstance.ConsumeCard(card);
        }
        else
        {
            // put the card back or something
        }
    }

    void CalamityClockBroken()
    {
        GameOverMenuInstance.Show();
    }
}
