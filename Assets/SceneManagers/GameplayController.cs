using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    const string ReactorIsEmptyLabel = "Reactor is Empty!!";
    const string ReactorIsOverflowingLabel = "Reactor is Overflowing!!";
    const string HandTooFullLabel = "Hand is too Full!";

    public ResourceRequesterUI ResourceRequesterUIInstance;
    public SequencerController SequencerControllerInstance;
    public PlayFieldController PlayFieldControllerInstance;
    public HandController HandControllerInstance;
    public ResourceController ResourceControllerInstance;

    public CalamityClock CalamityClockInstance;
    public GameOverMenu GameOverMenuInstance;

    public Reactor ReactorInstance; // todo: there are going to be multiple reactors, this is just a temporary measure

    private void Start()
    {
        HandControllerInstance.Initiate(
            () => { CalamityClockInstance.AddReason(HandTooFullLabel); }, 
            () => { CalamityClockInstance.RemoveReason(HandTooFullLabel); });

        ReactorInstance.Initiate(
            15f,
            () => { CalamityClockInstance.AddReason(ReactorIsEmptyLabel); },
            () => { CalamityClockInstance.RemoveReason(ReactorIsEmptyLabel); },
            () => { CalamityClockInstance.AddReason(ReactorIsOverflowingLabel); },
            () => { CalamityClockInstance.RemoveReason(ReactorIsOverflowingLabel); });

        CalamityClockInstance.Initiate(CalamityClockBroken);

        SequencerControllerInstance.Initiate(SequencerResourcePop);

        ResourceRequesterUIInstance.AddPossibleResource(ResourceControllerInstance.ColaFuel, ResourceRequested);
    }

    void ResourceRequested(GameResource resource)
    {
        HandControllerInstance.SpawnResourceCard(resource, ResourceCardDropped);
        SequencerControllerInstance.AddResource(resource);
    }

    void SequencerResourcePop(GameResource resource)
    {
        HandControllerInstance.SpawnResourceCard(resource, ResourceCardDropped);
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
