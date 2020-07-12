using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayFieldController : MonoBehaviour
{
    public MouseInputController MouseInputControllerInstance;

    public List<Transform> PossibleReactorSpots;
    List<Reactor> Reactors { get; set; } = new List<Reactor>();

    Action ReactorEmptyStartCallback { get; set; }
    Action ReactorEmptyEndCallback { get; set; }
    Action ReactorOverflowStartCallback { get; set; }
    Action ReactorOverflowEndCallback { get; set; }

    public void Initiate(Action reactorEmptyStartCallback, Action reactorEmptyEndCallback, Action reactorOverflowStartCallback, Action reactorOverflowEndCallback, Action clickCallback)
    {
        ReactorEmptyStartCallback = reactorEmptyStartCallback;
        ReactorEmptyEndCallback = reactorEmptyEndCallback;
        ReactorOverflowStartCallback = reactorOverflowStartCallback;
        ReactorOverflowEndCallback = reactorOverflowEndCallback;

        MouseInputControllerInstance.Initiate(clickCallback);
    }

    public Reactor GetHoveredReactor()
    {
        return MouseInputControllerInstance.GetHoveredReactor();
    }

    public void ResourceSelected(GameResource selected)
    {
        MouseInputControllerInstance.ResourceSelected(selected);
    }

    public void AddBuilding(Reactor building)
    {
        if (!PossibleReactorSpots.Any())
        {
            Debug.Log("Tried to spawn a building, but there's no spaces left for them");
            return;
        }

        int reactorSpot = UnityEngine.Random.Range(0, PossibleReactorSpots.Count);

        Reactor newReactor = Instantiate(building, PossibleReactorSpots[reactorSpot]);
        newReactor.Initiate(newReactor.MaxFuel / 2f, ReactorEmptyStartCallback, ReactorEmptyEndCallback, ReactorOverflowStartCallback, ReactorOverflowEndCallback);
        PossibleReactorSpots.RemoveAt(reactorSpot);
    }
}
