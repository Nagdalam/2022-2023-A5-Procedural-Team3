using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

public class BonusHealth : BonusStat
{
    public float bonusHealth = 0;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Stats stats))
        {
            stats.SetEnhancement(this);
            Destroy(gameObject);
        }
    }
}
