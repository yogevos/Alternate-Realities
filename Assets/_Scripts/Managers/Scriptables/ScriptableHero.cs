using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scriptable Hero")]
public class ScriptableHero : ScriptableUnitBase
{
    public HeroType heroType;
}

[Serializable]
public enum HeroType
{
    Player = 0,
    Enemy = 1,
}
