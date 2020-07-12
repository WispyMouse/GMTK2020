using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static bool GameIsStopped { get; set; } = false;

    const string ReactorIsEmptyLabel = "Reactor is Empty!!";
    const string ReactorIsOverflowingLabel = "Reactor is Overflowing!!";
    const string HandTooFullLabel = "Hand is too Full!";

    public GameObject GamePlay;

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

    public void HideStuff()
    {
        SequencerControllerInstance.Hide();
        HandControllerInstance.Hide();
        GamePlay.SetActive(false);
        GameIsStopped = true;
    }

    public void StartGamePlay()
    {
        GameIsStopped = false;
        GamePlay.SetActive(true);
        SequencerControllerInstance.Show();
        HandControllerInstance.Show();

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
            () => { CalamityClockInstance.RemoveReason(ReactorIsOverflowingLabel); },
            MouseClicked);

        // trigger any Cycle Number 0 events;
        // only planning on having this be where you get cola from
        foreach (CycleEvent curEvent in CycleEvents)
        {
            if (curEvent.CycleNumber == 0)
            {
                curEvent.ApplyEvent(OperationHandlerInstance);
            }
        }
    }

    void ResourceRequested(GameResource resource)
    {
        HandControllerInstance.AcquireResourceCardFromBoard(resource);
        SequencerControllerInstance.AddResource(resource);
        PlayFieldControllerInstance.ResourceSelected(HandControllerInstance.GetActiveCard().RepresentedResource);
    }

    void SequencerResourcePop(GameResource resource)
    {
        HandControllerInstance.SpawnResourceCardFromSequencer(resource);
        PlayFieldControllerInstance.ResourceSelected(HandControllerInstance.GetActiveCard().RepresentedResource);
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

    void MouseClicked()
    {
        if (HandControllerInstance.GetActiveCard() != null && PlayFieldControllerInstance.GetHoveredReactor() != null)
        {
            PlayFieldControllerInstance.GetHoveredReactor().Fuel(HandControllerInstance.GetActiveCard().RepresentedResource);
            HandControllerInstance.ConsumeActiveCard();
            PlayFieldControllerInstance.ResourceSelected(HandControllerInstance.GetActiveCard()?.RepresentedResource);
        }
    }
}
