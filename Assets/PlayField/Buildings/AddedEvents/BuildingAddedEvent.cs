using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingAddedEvent", menuName = "WispyMouse/BuildingAddedEvent")]
public class BuildingAddedEvent : CycleEvent
{
    public Reactor BuildingAdded;
    public override void ApplyEvent(OperationHandler operationHandler)
    {
        if (!SuppressNotification)
        {
            operationHandler.CustomNotificationCallback(BuildingAdded.name, "New reactor added", operationHandler.ThumbsUpSprite);
        }
        operationHandler.BuildingAddedCallback(BuildingAdded);
    }
}
