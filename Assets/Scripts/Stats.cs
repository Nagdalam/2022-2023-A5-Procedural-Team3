using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;
using static Stats;

public class Stats : MonoBehaviour
{
    public enum EStats
    {
        ARMOR,
        HEALTH,
        WEAPON,
    }

    public struct SStats
    {
        public float armor;
        public float maxHealth;
    }

    public SStats stats;

    private void Awake()
    {

    }

    protected Coroutine setEnhanceArmorCor = null;
    protected Coroutine setEnhanceHealthCor = null;
    protected Coroutine setEnhanceWeaponCor = null;

    void Start()
    {
        if (TryGetComponent(out Life life))
        {
            life.onDeath += CustomStopCoroutine;
            /*life.onDeathIE += StopCoroutine;*/

        }
    }

    void Update()
    {
        
    }

    void CustomStopCoroutine()
    {
        setEnhanceArmorCor = null;
        setEnhanceHealthCor = null;
        setEnhanceWeaponCor = null;

        stats.armor = 0;
    }

    public void SetEnhancement(BonusArmor armor)
    {
        if (setEnhanceArmorCor != null)
            return;

        float initialArmor = 0;
        setEnhanceArmorCor = StartCoroutine(SetEnhancementCoroutine());
        IEnumerator SetEnhancementCoroutine()
        {
            initialArmor = stats.armor;
            stats.armor = armor.bonusArmor;
            yield return new WaitForSeconds(armor.Duration);
        }

        stats.armor = initialArmor;
        setEnhanceArmorCor = null;
    }

    public void SetEnhancement(BonusHealth bonusHeath)
    {
        if (setEnhanceArmorCor != null)
            return;

        float initialMaxHealth = 0;
        setEnhanceArmorCor = StartCoroutine(SetEnhancementCoroutine());
        IEnumerator SetEnhancementCoroutine()
        {
            initialMaxHealth = stats.maxHealth;
            stats.maxHealth = bonusHeath.bonusHealth;
            yield return new WaitForSeconds(bonusHeath.Duration);
        }

        stats.maxHealth = initialMaxHealth;
        setEnhanceArmorCor = null;
    }
}
