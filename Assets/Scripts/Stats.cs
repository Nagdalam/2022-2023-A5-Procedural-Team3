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
            stats.armor = armor.bonusArmor;
            yield return new WaitForSeconds(armor.Duration);
        }

        stats.armor = initialArmor;
        setEnhanceArmorCor = null;
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
        }

        stats.maxHealth = initialMaxHealth;
        setEnhanceArmorCor = null;
    }
}
