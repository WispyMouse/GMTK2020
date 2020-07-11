using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reactor : MonoBehaviour
{
    enum ReactorState { Empty, OK, Overflow }
    ReactorState curReactorState { get; set; } = ReactorState.OK;

    public ResourceBar FuelBar;

    public float MaxFuel { get; set; } = 30f;
    float CurFuel { get; set; } = 30f;
    System.Action ReactorEmptyStartCallback { get; set; }
    System.Action ReactorEmptyEndCallback { get; set; }
    System.Action ReactorOverflowStartCallback { get; set; }
    System.Action ReactorOverflowEndCallback { get; set; }

    public void Initiate(float initialValue, System.Action reactorEmptyStartCallback, System.Action reactorEmptyEndCallback, System.Action reactorOverflowStartCallback, System.Action reactorOverflowEndCallback)
    {
        CurFuel = initialValue;
        ReactorEmptyStartCallback = reactorEmptyStartCallback;
        ReactorEmptyEndCallback = reactorEmptyEndCallback;
        ReactorOverflowStartCallback = reactorOverflowStartCallback;
        ReactorOverflowEndCallback = reactorOverflowEndCallback;

        FuelBar.SetLowValue(.1f);
        FuelBar.SetOverflowValue(1f);
    }

    private void Update()
    {
        if (GameplayController.GameIsStopped)
        {
            return;
        }

        CurFuel = Mathf.Max(0, CurFuel - Time.deltaTime);
        FuelBar.SetValue(CurFuel / MaxFuel);

        if (CurFuel <= 0 && curReactorState != ReactorState.Empty)
        {
            ReactorEmptyStartCallback();
            curReactorState = ReactorState.Empty;
        }
        else if (CurFuel > 0 && curReactorState == ReactorState.Empty)
        {
            ReactorEmptyEndCallback();
            curReactorState = ReactorState.OK;
        }
        else if (CurFuel > MaxFuel && curReactorState != ReactorState.Overflow)
        {
            ReactorOverflowStartCallback();
            curReactorState = ReactorState.Overflow;
        }
        else if (CurFuel <= MaxFuel && curReactorState == ReactorState.Overflow)
        {
            ReactorOverflowEndCallback();
            curReactorState = ReactorState.OK;
        }
    }

    public void Fuel(ResourceCard fromResource)
    {
        CurFuel = CurFuel + 10f;
    }
}
