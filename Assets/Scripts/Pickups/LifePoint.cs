using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePoint : Pickup
{
    public uint healAmount = 0;

    private void Awake()
    {
        if (healAmount <= 0)
            healAmount = 1;

    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Life colliderLife))
        {
            colliderLife.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
