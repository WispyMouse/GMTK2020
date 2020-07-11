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

    public ResourceEffectType ThisEffectType;
    public float EffectIntensity;
}
