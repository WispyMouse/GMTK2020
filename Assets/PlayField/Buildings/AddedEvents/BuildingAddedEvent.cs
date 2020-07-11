using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingAddedEvent", menuName = "WispyMouse/BuildingAddedEvent")]
public class BuildingAddedEvent : CycleEvent
{
    public Reactor BuildingAdded;
    public string Title;
    public string Description;

    public override void ApplyEvent(OperationHandler operationHandler)
    {
        if (!SuppressNotification)
        {
            operationHandler.CustomNotificationCallback(Title, string.Format(Description, BuildingAdded.MaxFuel, BuildingAdded.DrainRate), operationHandler.ThumbsUpSprite);
        }
        operationHandler.BuildingAddedCallback(BuildingAdded);
    }
}
