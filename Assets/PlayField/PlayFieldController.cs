using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayFieldController : MonoBehaviour
{
    public MouseInputController MouseInputControllerInstance;
    public ParticleController ParticleControllerInstance;

    public List<Reactor> Reactors;

    public void Initiate(Action reactorEmptyStartCallback, Action reactorEmptyEndCallback, Action reactorOverflowStartCallback, Action reactorOverflowEndCallback, Action clickCallback, Action rightClickCallback)
    {
        MouseInputControllerInstance.Initiate(clickCallback, rightClickCallback);

        foreach (Reactor curReactor in Reactors)
        {
            curReactor.Initiate(5f, reactorEmptyStartCallback, reactorEmptyEndCallback, reactorOverflowStartCallback, reactorOverflowEndCallback, ParticleControllerInstance);
        }

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
