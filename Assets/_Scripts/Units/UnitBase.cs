using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public Stats _stats { get; private set; }
    public virtual void SetStats(Stats stats) => _stats = stats;

    public virtual void TakeDamage(int dmg)
    {

    }
}
