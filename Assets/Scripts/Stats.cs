using System;
using System.Collections;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public enum EStats
    {
        ARMOR,
        HEALTH,
        WEAPON,
    }

    [System.Serializable]
    public struct SStats
    {
        [SerializeField] [Range(0, 100)] public float armor;
        [SerializeField] [Range(0, 10)] public float maxHealth;
        [SerializeField] public PlayerWeapons currentWeapon;

    }

    [SerializeField] public SStats stats;
    private SStats initialStats;

    private void Awake()
    {
        initialStats.armor = stats.armor;
        initialStats.maxHealth = stats.maxHealth;

    }

    protected Coroutine setEnhanceArmorCor = null;
    protected Coroutine setEnhanceHealthCor = null;
    protected Coroutine setEnhanceWeaponCor = null;

    public Action onEnhanceArmor;
    public Action onEnhanceHealth;
    public Action onEnhanceWeapon;

    public Action onUnenhanceArmor;
    public Action onUnenhanceHealth;
    public Action onUnenhanceWeapon;

    void Start()
    {
        if (TryGetComponent(out Life life))
        {
            life.onDeath += CustomStopCoroutine;
            /*life.onDeathIE += StopCoroutine;*/

        }

        var shooter = gameObject.GetComponentInChildren<Shooter>();
       /* if (shooter)
        {
            shooter.onSwitchWeapon += (weapon) => { setEnhanceWeaponCor = null; };
        }*/
    }

    void Update()
    {
        
    }

    void CustomStopCoroutine()
    {
        setEnhanceArmorCor = null;
        setEnhanceHealthCor = null;
        setEnhanceWeaponCor = null;

        stats.armor = initialStats.armor;
        stats.maxHealth = initialStats.maxHealth;
    }

    public void SetEnhancement(BonusArmor armor)
    {
        if (setEnhanceArmorCor != null)
            return;

        float initialArmor = stats.armor;
        setEnhanceArmorCor = StartCoroutine(SetEnhancementCoroutine());
        IEnumerator SetEnhancementCoroutine()
        {
            //Events
            onEnhanceArmor?.Invoke();
            onUnenhanceWeapon?.Invoke();

            stats.armor = armor.bonusArmor;
            /*Debug.Log($"Buffed : {stats.armor}");*/

            yield return new WaitForSeconds(armor.Duration);

            onUnenhanceArmor?.Invoke();
            stats.armor = initialArmor;
            /*Debug.Log($"UnBuffed : {stats.armor}");*/
            setEnhanceArmorCor = null;
        }
    }

    public void SetEnhancement(BonusHealth bonusHeath)
    {
        if (setEnhanceHealthCor != null)
            return;

        float initialMaxHealth = stats.maxHealth;
        setEnhanceHealthCor = StartCoroutine(SetEnhancementCoroutine());
        IEnumerator SetEnhancementCoroutine()
        {
            stats.maxHealth = bonusHeath.bonusHealth;
            yield return new WaitForSeconds(bonusHeath.Duration);
            stats.maxHealth = initialMaxHealth;
            setEnhanceArmorCor = null;
        }
    }

    public void SetEnhancement(BonusWeapon bonusWeapon, Shooter shooter)
    {
        if (setEnhanceWeaponCor != null)
            return;

        var initialWeapon = shooter.currentWeapon;
        setEnhanceWeaponCor = StartCoroutine(SetEnhancementCoroutine());
        IEnumerator SetEnhancementCoroutine()
        {
            //Events
            onEnhanceWeapon?.Invoke();
            onUnenhanceArmor?.Invoke();

            shooter.currentWeapon = bonusWeapon.playerWeapon;
            shooter.SetAllStat();
            /*Debug.Log($"Buffed : {shooter.currentWeapon.weaponFireRate}");*/

            yield return new WaitForSeconds(bonusWeapon.Duration);

            onUnenhanceWeapon?.Invoke();
            shooter.currentWeapon = initialWeapon;
            shooter.SetAllStat();
            /*Debug.Log($"UnBuffed : {shooter.currentWeapon.weaponFireRate}");*/
            setEnhanceWeaponCor = null; 

            /* shooter.SetAllStat(bonusWeapon.playerWeapon);
             Debug.Log($"Buffed : {shooter.currentWeapon.weaponFireRate}");
             yield return new WaitForSeconds(bonusWeapon.Duration);
             shooter.SetAllStat(initialWeapon);
             Debug.Log($"UnBuffed : {shooter.currentWeapon.weaponFireRate}");
             setEnhanceWeaponCor = null;*/

        }
    }
}
