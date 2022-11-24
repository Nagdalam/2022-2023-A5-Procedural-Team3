using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconWeapon : Icon
{
    void Awake()
    {
        base.Awake();

        var stat = GetComponentInParent<Stats>();
        if (stat)
        {
            stat.onEnhanceWeapon += Activation;
            stat.onUnenhanceWeapon += Deactivation;
        }
    }
}
