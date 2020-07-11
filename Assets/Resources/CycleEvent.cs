using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CycleEvent : ScriptableObject
{
    public int CycleNumber;
    public bool SuppressNotification;

    public abstract void ApplyEvent(OperationHandler operationHandler);
}
