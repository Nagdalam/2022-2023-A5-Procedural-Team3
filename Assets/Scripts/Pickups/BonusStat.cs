using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusStat : Pickup
{
    protected void Awake()
    {
        
    }

    private float duration = 0;
    public float Duration { get => duration; set => duration = value; }
}
