using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PlayerWeapons weapon = null;

    protected void OnTriggerEnter2D(Collider2D col)
    {
        var shooter = col.GetComponentInChildren<Shooter>();
        if (!shooter)
            return;

        shooter.currentWeapon = weapon;
        shooter.SwitchWeapon();
    }
}