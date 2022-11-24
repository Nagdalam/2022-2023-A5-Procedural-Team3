using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusWeapon : BonusStat
{
    public PlayerWeapons playerWeapon = null;
    protected Coroutine setEnhanceWeaponCor = null;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        var shooter = col.GetComponentInChildren<Shooter>();
        if (!shooter)
            return;

        if (col.TryGetComponent(out Stats stats))
        {
            stats.SetEnhancement(this, shooter);
            Destroy(gameObject);
        }

        /*   var shooter = col.GetComponentInChildren<Shooter>();
           if (!shooter)
               return;

           EnhanceWeapon(shooter);*/
    }

    /*void EnhanceWeapon(Shooter shooter)
    {
        if (setEnhanceWeaponCor != null)
            return;

        var initialWeapon = shooter.currentWeapon;
        setEnhanceWeaponCor = StartCoroutine(SetEnhancementCoroutine());
        IEnumerator SetEnhancementCoroutine()
        {
            shooter.SetAllStat(playerWeapon);
            yield return new WaitForSeconds(Duration);
            shooter.SetAllStat(initialWeapon);
        }
    }*/
}
