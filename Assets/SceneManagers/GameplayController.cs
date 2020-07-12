﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static bool GameIsStopped { get; set; } = false;

    const string ReactorIsEmptyLabel = "Reactor is Empty!!";
    const string ReactorIsOverflowingLabel = "Reactor is Overflowing!!";
    const string HandTooFullLabel = "Hand is too Full!";

    public GameObject GamePlay;

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
            AddResource);

        PlayFieldControllerInstance.Initiate(
            () => { CalamityClockInstance.AddReason(ReactorIsEmptyLabel); },
            () => { CalamityClockInstance.RemoveReason(ReactorIsEmptyLabel); },
            () => { CalamityClockInstance.AddReason(ReactorIsOverflowingLabel); },
            () => { CalamityClockInstance.RemoveReason(ReactorIsOverflowingLabel); },
            MouseClicked,
            RightMouseClicked);

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

        bool eventApplied = false;
        foreach (CycleEvent curEvent in CycleEvents.Where(cycle => cycle.MyCycleEventType == CycleEventType.Set && cycle.CycleNumber == CurCycle))
        {
            curEvent.ApplyEvent(OperationHandlerInstance);
            eventApplied = true;
        }

        if (!eventApplied)
        {
            List<CycleEvent> eligibleEvents = CycleEvents.Where(cycle => cycle.CycleNumber >= CurCycle && cycle.MyCycleEventType == CycleEventType.Random).ToList();
            
            if (eligibleEvents.Any())
            {
                int chosenEvent = Random.Range(0, eligibleEvents.Count);
                eligibleEvents[chosenEvent].ApplyEvent(OperationHandlerInstance);
            }
        }
    }

    void AddResource(GameResource toAdd)
    {
        HandControllerInstance.AcquireResourceCardFromBoard(toAdd);
        SequencerControllerInstance.AddResource(toAdd);
        PlayFieldControllerInstance.ResourceSelected(HandControllerInstance.GetActiveCard().RepresentedResource);
    }

    void MouseClicked()
    {
        ResourceCard activeCard = HandControllerInstance.GetActiveCard();
        Reactor reactor = PlayFieldControllerInstance.GetHoveredReactor();

        if (activeCard != null && reactor != null && reactor.Activated)
        {
            reactor.Fuel(activeCard.RepresentedResource);
            HandControllerInstance.ConsumeActiveCard();
            PlayFieldControllerInstance.ResourceSelected(HandControllerInstance.GetActiveCard()?.RepresentedResource);
        }
    }

    void RightMouseClicked()
    {
        Reactor reactor = PlayFieldControllerInstance.GetHoveredReactor();

        if (reactor != null && !reactor.Activated)
        {
            reactor.BecomeActivated();
            NotificationControllerInstance.SpawnNotification(reactor);
        }
    }
}
