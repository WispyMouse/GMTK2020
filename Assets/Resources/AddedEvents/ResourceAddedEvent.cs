using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceAddedEvent", menuName = "WispyMouse/ResourceAddedEvent")]
public class ResourceAddedEvent : CycleEvent
{
    public GameResource ResourceAdded;
    public int Amount = 1;

    public override void ApplyEvent(OperationHandler operationHandler)
    {
        if (!SuppressNotification)
        {
            operationHandler.ResourceNotificationCallback(ResourceAdded);
        }
        for (int ii = 0; ii < Amount; ii++)
        {
            operationHandler.ResourceAddedCallback(ResourceAdded);
        }
    }
}
