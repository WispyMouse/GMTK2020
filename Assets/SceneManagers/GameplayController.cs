using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static bool GameIsStopped { get; set; } = false;

    const string ReactorIsEmptyLabel = "Reactor is Empty!!";
    const string ReactorIsOverflowingLabel = "Reactor is Overflowing!!";
    const string HandTooFullLabel = "Hand is too Full!";

    public ResourceRequesterUI ResourceRequesterUIInstance;
    public SequencerController SequencerControllerInstance;
    public PlayFieldController PlayFieldControllerInstance;
    public HandController HandControllerInstance;
    public NotificationController NotificationControllerInstance;

    public CalamityClock CalamityClockInstance;
    public GameOverMenu GameOverMenuInstance;

    public Reactor ReactorPF;

    public List<CycleEvent> CycleEvents;
    OperationHandler OperationHandlerInstance;
    public Sprite ThumbsUpSprite;

    int CurCycle { get; set; } = 0;

    private void Start()
    {
        HandControllerInstance.Initiate(
            () => { CalamityClockInstance.AddReason(HandTooFullLabel); }, 
            () => { CalamityClockInstance.RemoveReason(HandTooFullLabel); });

        CalamityClockInstance.Initiate(CalamityClockBroken);

        SequencerControllerInstance.Initiate(SequencerResourcePop, CyclePasses);

        OperationHandlerInstance.Initiate(ThumbsUpSprite,
            NotificationControllerInstance.SpawnNotification,
            NotificationControllerInstance.SpawnNotification,
            AddResource,
            AddBuilding
            );

        PlayFieldControllerInstance.Initiate(
            () => { CalamityClockInstance.AddReason(ReactorIsEmptyLabel); },
            () => { CalamityClockInstance.RemoveReason(ReactorIsEmptyLabel); },
            () => { CalamityClockInstance.AddReason(ReactorIsOverflowingLabel); },
            () => { CalamityClockInstance.RemoveReason(ReactorIsOverflowingLabel); });

        // trigger any Cycle Number 0 events;
        // only planning on having this be where you get cola from
        foreach (CycleEvent curEvent in CycleEvents)
        {
            if (curEvent.CycleNumber == 0)
            {
                curEvent.ApplyEvent(OperationHandlerInstance);
            }
        }

        GameIsStopped = false;
    }

    void ResourceRequested(GameResource resource)
    {
        HandControllerInstance.AcquireResourceCardFromBoard(resource, ResourceCardDropped);
        SequencerControllerInstance.AddResource(resource);
    }

    void SequencerResourcePop(GameResource resource)
    {
        HandControllerInstance.SpawnResourceCardFromSequencer(resource, ResourceCardDropped);
    }

    void ResourceCardDropped(ResourceCard card)
    {
        Reactor hoveredReactor = PlayFieldControllerInstance.GetHoveredReactor();
        
        if (hoveredReactor != null)
        {
            // apply the effect of the card or something
            hoveredReactor.Fuel(card.RepresentedResource);
            HandControllerInstance.ConsumeCard(card);
        }
        else
        {
            // put the card back or something
        }
    }

    void CalamityClockBroken()
    {
        GameIsStopped = true;
        GameOverMenuInstance.Show(CurCycle);
    }

    void CyclePasses()
    {
        CurCycle++;
        Debug.Log($"Cycle Lasted, Now On: {CurCycle}");

        foreach (CycleEvent curEvent in CycleEvents)
        {
            if (curEvent.CycleNumber == CurCycle)
            {
                curEvent.ApplyEvent(OperationHandlerInstance);
            }
        }
    }

    void AddResource(GameResource toAdd)
    {
        ResourceRequesterUIInstance.AddPossibleResource(toAdd, ResourceRequested);
    }

    void AddBuilding(Reactor building)
    {
        PlayFieldControllerInstance.AddBuilding(building);
    }
}
