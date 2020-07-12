using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CycleEventType { Random, Set}

public abstract class CycleEvent : ScriptableObject
{
    public CycleEventType MyCycleEventType;
    public int CycleNumber;
    public bool SuppressNotification;

    public abstract void ApplyEvent(OperationHandler operationHandler);
}
