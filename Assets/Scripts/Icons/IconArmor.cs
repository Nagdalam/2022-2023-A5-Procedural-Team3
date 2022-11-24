using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconArmor : Icon
{
    void Awake()
    {
        base.Awake();

        var stat = GetComponentInParent<Stats>();
        if (stat)
        {
            stat.onEnhanceArmor += Activation;
            stat.onUnenhanceArmor += Deactivation;
        }
    }
}
