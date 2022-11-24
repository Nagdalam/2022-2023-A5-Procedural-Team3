using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Pickup
{
    public PlayerWeapons playerWeapon = null;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        var shooter = col.GetComponentInChildren<Shooter>();
    }
}
