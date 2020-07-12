using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceEffectType { Fuel }

[CreateAssetMenu(fileName = "newresource", menuName = "WispyMouse/GameResource")]
public class GameResource : ScriptableObject
{
    public string ResourceName;
    public string Description;
    public Sprite Graphic;
    public AudioClip ActiveCardSound;

    public bool IsSugar;
    public bool IsCarb;
    public bool IsCaffeine;

    public ResourceEffectType ThisEffectType;
    public float EffectIntensity;
}
