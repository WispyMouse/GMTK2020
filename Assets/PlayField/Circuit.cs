using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuit : MonoBehaviour
{
    public SpriteRenderer Light;
    float flickerTimer { get; set; } = 0;

    public Color OkayColor;
    public Color OffColor;
    public Color OverflowingColor;

    Color ColorOne { get; set; }
    Color ColorTwo { get; set; }

    private void Start()
    {
        ColorOne = OffColor;
        ColorTwo = OffColor;
        Light.color = OffColor;
    }

    private void Update()
    {
        flickerTimer += Time.deltaTime;
        Light.color = Color.Lerp(ColorOne, ColorTwo, Mathf.PingPong(flickerTimer, 1f));
    }

    public void TurnOn()
    {
        ColorOne = OkayColor;
        ColorTwo = OkayColor;
    }

    public void ProblemSolved()
    {
        ColorOne = OkayColor;
        ColorTwo = OkayColor;
    }

    public void Overloading()
    {
        ColorOne = OkayColor;
        ColorTwo = OverflowingColor;
    }

    public void Empty()
    {
        ColorOne = OkayColor;
        ColorTwo = OffColor;
    }
}
