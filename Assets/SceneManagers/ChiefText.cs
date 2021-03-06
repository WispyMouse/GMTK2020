﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChiefText", menuName = "WispyMouse/ChiefText")]
public class ChiefText : ScriptableObject
{
    public int CycleCount;
    [TextArea]
    public string Text;
    public string LetterGrade;
    public Color LetterColor;
}
