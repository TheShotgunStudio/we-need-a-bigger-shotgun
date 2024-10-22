using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : ScriptableObject
{
    public float Health;
    public float Attack;
    public float Speed;

    /// <summary>
    /// Clone the stats so that the ScriptableObject doesn't get modified
    /// </summary>
    /// <returns>A copy of the Stats</returns>
    public Stats Clone()
    {
        Stats clone = Instantiate(this);
        return clone;
    }
}
