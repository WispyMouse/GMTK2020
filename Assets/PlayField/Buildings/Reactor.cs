using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reactor : MonoBehaviour
{
    enum ReactorState { Empty, OK, Overflow }
    ReactorState curReactorState { get; set; } = ReactorState.OK;

    public ResourceBar FuelBar;
    GameResource HoveredResource { get; set; }

    public SpriteRenderer MyRenderer;
    public Sprite OfflineSprite;
    public Sprite OnlineSprite;

    float MaximumPossibleFuelForAnyReactor { get; set; } = 15f;
    public float MaxFuel { get; set; } = 5f;
    float CurFuel { get; set; } = 5f;
    public float DrainRate { get; set; } = .25f;

    public bool Activated { get; set; }

    System.Action ReactorEmptyStartCallback { get; set; }
    System.Action ReactorEmptyEndCallback { get; set; }
    System.Action ReactorOverflowStartCallback { get; set; }
    System.Action ReactorOverflowEndCallback { get; set; }

    public void Initiate(float initialValue, 
        System.Action reactorEmptyStartCallback, 
        System.Action reactorEmptyEndCallback, 
        System.Action reactorOverflowStartCallback, 
        System.Action reactorOverflowEndCallback)
    {
        CurFuel = initialValue;
        ReactorEmptyStartCallback = reactorEmptyStartCallback;
        ReactorEmptyEndCallback = reactorEmptyEndCallback;
        ReactorOverflowStartCallback = reactorOverflowStartCallback;
        ReactorOverflowEndCallback = reactorOverflowEndCallback;
    }

    private void Update()
    {
        if (GameplayController.GameIsStopped || !Activated)
        {
            return;
        }

        float fillAmount = 0;

        if (HoveredResource != null && HoveredResource.ThisEffectType == ResourceEffectType.Fuel)
        {
            fillAmount = HoveredResource.EffectIntensity;
        }

        CurFuel = Mathf.Max(0, CurFuel - Time.deltaTime * DrainRate);
        FuelBar.SetValue(CurFuel, MaxFuel, MaximumPossibleFuelForAnyReactor, fillAmount);

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

    public void BeingHovered(GameResource fromResource)
    {
        HoveredResource = fromResource;
    }

    public void EndHovered()
    {
        HoveredResource = null;
    }

    public void Fuel(GameResource fromResource)
    {
        switch (fromResource.ThisEffectType)
        {
            case ResourceEffectType.Fuel:
                CurFuel = CurFuel + fromResource.EffectIntensity;
                break;
        }
    }

    public void StartDeactivated()
    {
        Activated = false;
        MyRenderer.sprite = OfflineSprite;
        FuelBar.Hide();
    }

    public void StartActivated()
    {
        Activated = true;
        MyRenderer.sprite = OnlineSprite;
        FuelBar.Show();
    }

    public void BecomeActivated()
    {
        Activated = true;
        MyRenderer.sprite = OnlineSprite;
        FuelBar.Show();
    }
}
