using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reactor : MonoBehaviour
{
    public ResourceBar FuelBar;

    float MaxFuel { get; set; } = 30f;
    float CurFuel { get; set; } = 30f;

    private void Start()
    {
        CurFuel = MaxFuel;
    }

    private void Update()
    {
        CurFuel = Mathf.Max(0, CurFuel - Time.deltaTime);
        FuelBar.SetValue(CurFuel / MaxFuel);
    }

    public void Fuel(ResourceCard fromResource)
    {
        CurFuel = Mathf.Min(MaxFuel, CurFuel + 10f);
    }
}
