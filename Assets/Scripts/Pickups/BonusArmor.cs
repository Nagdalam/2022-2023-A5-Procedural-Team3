using UnityEngine;

public class BonusArmor : BonusStat
{
    public float bonusArmor = 0;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out Stats stats))
        {
            stats.SetEnhancement(this);
            Destroy(gameObject);
        }
    }
}
