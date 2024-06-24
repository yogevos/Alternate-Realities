using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class ScriptableUnitBase : ScriptableObject
{

    public Type type;

    [SerializeField] Stats _stats;
    public Stats BaseStats => _stats;

    // public PlayerUnitBase

    //Menu
    public string Description;
    public Sprite MenuSprite;
}

[Serializable]
public struct Stats
{
    public int Health;
    public int MaxHealth;
    public int AttackPower;
    public int TimeToShoot;
}
    public enum Type
    {
        Player = 0,
        Enemies = 1,
    }

