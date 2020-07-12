using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayFieldController : MonoBehaviour
{
    public MouseInputController MouseInputControllerInstance;

    public List<Reactor> Reactors;

    Action ReactorEmptyStartCallback { get; set; }
    Action ReactorEmptyEndCallback { get; set; }
    Action ReactorOverflowStartCallback { get; set; }
    Action ReactorOverflowEndCallback { get; set; }

    public void Initiate(Action reactorEmptyStartCallback, Action reactorEmptyEndCallback, Action reactorOverflowStartCallback, Action reactorOverflowEndCallback, Action clickCallback, Action rightClickCallback)
    {
        ReactorEmptyStartCallback = reactorEmptyStartCallback;
        ReactorEmptyEndCallback = reactorEmptyEndCallback;
        ReactorOverflowStartCallback = reactorOverflowStartCallback;
        ReactorOverflowEndCallback = reactorOverflowEndCallback;

        MouseInputControllerInstance.Initiate(clickCallback, rightClickCallback);

        int randomStart = UnityEngine.Random.Range(0, Reactors.Count);
        for (int ii = 0; ii < Reactors.Count; ii++)
        {
            if (ii == randomStart)
            {
                Reactors[ii].StartActivated();
            }
            else
            {
                Reactors[ii].StartDeactivated();
            }
        }
    }

    public Reactor GetHoveredReactor()
    {
        return MouseInputControllerInstance.GetHoveredReactor();
    }

    public void ResourceSelected(GameResource selected)
    {
        MouseInputControllerInstance.ResourceSelected(selected);
    }
}
