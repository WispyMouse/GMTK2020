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
    public List<Sprite> OnlineSprites;
    int Frame { get; set; } = 0;
    float FrameTime { get; set; } = 0;
    float TimePerFrame { get; } = .25f;
    float OverflowModifier { get; } = 3f;
    float EmptyModifier { get; } = .5f;

    public DetailsPane DetailsPaneInstance;

    float MaximumPossibleFuelForAnyReactor { get; set; } = 40f;
    public float MaxFuel { get; set; } = 20f;
    float CurFuel { get; set; } = 20f;
    public float DrainRate { get; set; } = 1f;

    public bool Activated { get; set; }

    public bool AcceptsSugar;
    public bool AcceptsCaffeine;
    public bool AcceptsCarb;

    public Circuit AttachedCircuit;

    System.Action ReactorEmptyStartCallback { get; set; }
    System.Action ReactorEmptyEndCallback { get; set; }
    System.Action ReactorOverflowStartCallback { get; set; }
    System.Action ReactorOverflowEndCallback { get; set; }

    ParticleController ParticleControllerInstance { get; set; }

    public void Initiate(float initialValue,
        System.Action reactorEmptyStartCallback,
        System.Action reactorEmptyEndCallback,
        System.Action reactorOverflowStartCallback,
        System.Action reactorOverflowEndCallback,
        ParticleController particleControllerInstance)
    {
        CurFuel = initialValue;
        ReactorEmptyStartCallback = reactorEmptyStartCallback;
        ReactorEmptyEndCallback = reactorEmptyEndCallback;
        ReactorOverflowStartCallback = reactorOverflowStartCallback;
        ReactorOverflowEndCallback = reactorOverflowEndCallback;
        ParticleControllerInstance = particleControllerInstance;

        DetailsPaneInstance.Initiate(this);
    }

    private void Update()
    {
        if (GameplayController.GameIsStopped || !Activated)
        {
            return;
        }

        FrameTime += Time.deltaTime * (IsEmpty ? EmptyModifier : IsOverflowing ? OverflowModifier : 1f);

        if (FrameTime >= TimePerFrame)
        {
            FrameTime -= TimePerFrame;
            Frame = (Frame + 1) % OnlineSprites.Count;
            MyRenderer.sprite = OnlineSprites[Frame];
        }

        float fillAmount = 0;

        if (HoveredResource != null && HoveredResource.ThisEffectType == ResourceEffectType.Fuel)
        {
            fillAmount = HoveredResource.EffectIntensity;
        }

        CurFuel = Mathf.Max(0, CurFuel - Time.deltaTime * DrainRate);
        FuelBar.SetValue(CurFuel, MaxFuel, MaximumPossibleFuelForAnyReactor, fillAmount);

        if (IsEmpty && curReactorState != ReactorState.Empty)
        {
            ParticleControllerInstance.StartSweatParticle(this);
            AttachedCircuit.Empty();
            ReactorEmptyStartCallback();
            curReactorState = ReactorState.Empty;
        }
        else if (!IsEmpty && curReactorState == ReactorState.Empty)
        {
            ParticleControllerInstance.StopParticle(this);
            AttachedCircuit.ProblemSolved();
            ReactorEmptyEndCallback();
            curReactorState = ReactorState.OK;
        }
        else if (IsOverflowing && curReactorState != ReactorState.Overflow)
        {
            ParticleControllerInstance.StartSteamParticle(this);
            AttachedCircuit.Overloading();
            ReactorOverflowStartCallback();
            curReactorState = ReactorState.Overflow;
        }
        else if (!IsOverflowing && curReactorState == ReactorState.Overflow)
        {
            ParticleControllerInstance.StopParticle(this);
            AttachedCircuit.ProblemSolved();
            ReactorOverflowEndCallback();
            curReactorState = ReactorState.OK;
        }
    }

    public void BeingHovered(GameResource fromResource)
    {
        if (!Activated)
        {
            DetailsPaneInstance.Show();
        }

        if (fromResource != null && !Accepts(fromResource))
        {
            return;
        }

        HoveredResource = fromResource;
    }

    public void EndHovered()
    {
        DetailsPaneInstance.Hide();
        HoveredResource = null;
    }

    public void Fuel(GameResource fromResource)
    {
        Debug.Log(fromResource != null);
        if (!Accepts(fromResource))
        {
            return;
        }

        ParticleControllerInstance.PlayResourceParticle(this, fromResource);

        switch (fromResource.ThisEffectType)
        {
            case ResourceEffectType.Fuel:
                CurFuel = CurFuel + fromResource.EffectIntensity;
                break;
        }

        HoveredResource = null;
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
        CurFuel = MaxFuel * .75f;
        MyRenderer.sprite = OnlineSprites[0];
        FuelBar.Show();
        AttachedCircuit.TurnOn();
    }

    public void BecomeActivated()
    {
        DetailsPaneInstance.Hide();
        CurFuel = MaxFuel * .5f;
        Activated = true;
        MyRenderer.sprite = OnlineSprites[0];
        FuelBar.Show();
        AttachedCircuit.TurnOn();
    }

    public bool Accepts(GameResource resource)
    {
        return (resource.IsSugar && AcceptsSugar) ||
            (resource.IsCaffeine && AcceptsCaffeine) ||
            (resource.IsCarb && AcceptsCarb);
    }

    bool IsOverflowing
    {
        get
        {
            return CurFuel > MaxFuel;
        }
    }

    bool IsEmpty
    {
        get
        {
            return CurFuel <= 0;
        }
    }
}
