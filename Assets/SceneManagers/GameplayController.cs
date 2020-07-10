using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public ResourceRequesterUI ResourceRequesterUIInstance;
    public SequencerController SequencerControllerInstance;
    public HandController HandControllerInstance;

    private void Start()
    {
        ResourceRequesterUIInstance.AddPossibleResource("Don't Overload Pills", ResourceRequested);
        ResourceRequesterUIInstance.AddPossibleResource("Power Plant Juice", ResourceRequested);
    }

    void ResourceRequested(string resource)
    {
        HandControllerInstance.SpawnResourceCard(resource);
        SequencerControllerInstance.AddResource(resource);
    }
}
